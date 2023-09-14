using JackLib;
using JackStockApi.Dtos;
using JackStockApi.Repositorys;

namespace JackStockApi.Services
{
    public class TwseService
    {
        private readonly TwseRepo _twseRepo;
        private readonly StockService _stockService;
        public TwseService(TwseRepo twseRepo, StockService stockService)
        {
            _twseRepo = twseRepo;
            _stockService = stockService;
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

            Dictionary<string, string> paras = new Dictionary<string, string>
            {
                { "stockNo", stockCode },
            };
            if (!date.IsNullOrEmpty()) paras.Add("date", date);
            var stockDay = await _twseRepo.GetTwseStockDayAsync(paras);
            await stock;

            if (stockDay != null 
                && stockDay.Total > 0 
                && stockDay.Stat.Equals("OK") 
                && stockDay.Title.Contains(stockCode))
            {
                List<StockDayHistoryDto> result = new List<StockDayHistoryDto>(stockDay.Total);
                foreach(var item in stockDay.Data)
                {
                    result.Add(new StockDayHistoryDto()
                    {
                        DateTime = item[0].RocToAd(),
                        TradeVolumn = Convert.ToInt64(item[1].Replace(",", "")),
                        TradeValue = Convert.ToInt64(item[2].Replace(",", "")),
                        OpeningPrice = Convert.ToDouble(item[3]),
                        HighestPrice = Convert.ToDouble(item[4]),
                        LowestPrice = Convert.ToDouble(item[5]),
                        ClosingPrice = Convert.ToDouble(item[6]),
                        Change = item[7].StartsWith("X") ? Convert.ToDouble(item[3]) - Convert.ToDouble(item[6]) : Convert.ToDouble(item[7]),
                        Transaction = Convert.ToInt64(item[8].Replace(",", "")),
                        StockId = stock.Result.Id,
                    });
                }

                await _stockService.InsertBatchStockDayHisAsync(result);

                return result;
            }

            return null;
        }
    }
}
