using JackLib;
using JackStockApi.Domain;
using JackStockApi.Dtos;
using JackStockApi.Repositorys;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;

namespace JackStockApi.Services
{
    public class TwseService
    {
        private readonly TwseRepo _twseRepo;
        private readonly StockService _stockService;
        private readonly ConcurrentQueue<DateTime> requestQueue = new ConcurrentQueue<DateTime>();
        private readonly object lockObject = new object();
        public TwseService(TwseRepo twseRepo, StockService stockService)
        {
            _twseRepo = twseRepo;
            _stockService = stockService;
        }

        private async Task MakeAPICall()
        {
            DateTime currentTime = DateTime.Now;

            // 檢查時間戳記並等待至少5秒
            Monitor.Enter(lockObject);
            try
            {
                if (requestQueue.TryPeek(out DateTime lastRequestTime) && (currentTime - lastRequestTime).TotalSeconds < 5)
                {
                    TimeSpan waitTime = TimeSpan.FromSeconds(5) - (currentTime - lastRequestTime);
                    await Task.Delay(waitTime);
                }

                requestQueue.Enqueue(currentTime);
            }
            finally
            {
                Monitor.Exit(lockObject);
            }

            // 執行 API 呼叫 (代碼在這裡執行)
            Console.WriteLine($"API 呼叫成功 - 時間：{currentTime}");

            // 從佇列中移除請求
            Monitor.Enter(lockObject);
            try
            {
                requestQueue.TryDequeue(out DateTime _);
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }
        /// <summary>
        /// 取得該股號當月歷史資料並寫入DB
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<IList<StockDayHistoryDto>?> GetTwseStockDayAndInsertToDbAsync(string stockCode, string? date)
        {
            var stock = _stockService.FindStockByCodeAsync(stockCode);

            Dictionary<string, string> paras = new Dictionary<string, string>(){ { "stockNo", stockCode }};
            if (!date.IsNullOrEmpty()) paras.Add("date", date);
            var stockDay = await _twseRepo.GetTwseStockDayAsync(paras);
            await stock;

            var result = TwseToStockDayHis(stockDay, stock.Result);
            if (result != null)
            {
                Debug.WriteLine($"Insert {paras["stockNo"]}-{paras["date"]}");
                await _stockService.InsertBatchStockDayHisAsync(result);
            }

            return result;
        }

        /// <summary>
        /// 取得所有股號並新增歷史資料至DB
        /// </summary>
        /// <returns></returns>
        public async Task GetTwseStockDayAllAndInsertToDbAsync()
        {
            var stocks = await _stockService.FindStockAllAsync();
            if (stocks == null) return;

            foreach (var stock in stocks)
            {
                if (stock.LastUpdateDate.Date >= DateTime.Now.Date) continue;
                var year = stock.LastUpdateDate.Year;
                var month = stock.LastUpdateDate.Month;
                var nowYear = DateTime.Now.Year;
                var nowMonth = DateTime.Now.Month;

                for(int y = year; y <= nowYear; y++)
                {
                    for (int m = 1; m <= 12; m++)
                    {
                        if (y == nowYear && m > nowMonth) break;
                        if (y == year && m < month) m = month;

                        while (true)
                        {
                            var insert = await GetTwseStockDayAndInsertToDbAsync(stock.Code, new DateOnly(y, m, 1).ToString("yyyyMMdd"));
                            SpinWait.SpinUntil(() => false, 5000);
                            if (insert != null) break;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Twse convert to StockDayHistoryDto
        /// </summary>
        /// <param name="stockDay"></param>
        /// <param name="stock"></param>
        /// <returns></returns>
        private IList<StockDayHistoryDto>? TwseToStockDayHis(TwseStockDayResponeDto stockDay, Stock stock)
        {
            if (stockDay != null
               && stockDay.Total > 0
               && stockDay.Stat.Equals("OK")
               && stockDay.Title.Contains(stock.Code)
               && stock != null)
            {
                return stockDay.ConvertToStockDayHisList(stock.Id);
            }

            return null;
        }
    }
}
