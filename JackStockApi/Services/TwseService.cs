using JackLib;
using JackStockApi.Dtos;
using JackStockApi.Repositorys;

namespace JackStockApi.Services
{
    public class TwseService
    {
        private readonly TwseRepo _repo;
        public TwseService(TwseRepo repo)
        {
            _repo = repo;
        }

        public async Task<IList<StockDayHistoryDto>?> GetTwseStockDayAsync(string stockCode, string date = "")
        {
            Dictionary<string, string> paras = new Dictionary<string, string>
            {
                { "stockNo", stockCode },
            };
            if (!date.IsNullOrEmpty()) paras.Add("date", date);
            var stockDay = await _repo.GetTwseStockDayAsync(paras);

            if (stockDay.Total > 0 && stockDay.Stat.Equals("OK") && stockDay.Title.Contains(stockCode))
            {
                List<StockDayHistoryDto> result = new List<StockDayHistoryDto>();
                foreach(var item in stockDay.Data)
                {
                    result.Add(new StockDayHistoryDto()
                    {
                        DateTime = item[0].RocToAd(),
                        TradeVolumn = Convert.ToInt32(item[1].Replace(",", "")),
                        TradeValue = Convert.ToInt32(item[2].Replace(",", "")),
                        OpeningPrice = Convert.ToDouble(item[3]),
                        HighestPrice = Convert.ToDouble(item[4]),
                        LowestPrice = Convert.ToDouble(item[5]),
                        ClosingPrice = Convert.ToDouble(item[6]),
                        Change = Convert.ToDouble(item[7]),
                        Transaction = Convert.ToInt32(item[8].Replace(",", "")),
                    });
                }

                return result;
            }

            return null;
        }
    }
}
