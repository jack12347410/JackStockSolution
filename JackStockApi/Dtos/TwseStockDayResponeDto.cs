using JackLib;
using JackStockApi.Domain;

namespace JackStockApi.Dtos
{
    public class TwseStockDayResponeDto
    {
        public string Stat { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string[] Fields { get; set; }
        public string[][] Data { get; set; }
        public string[] Notes { get; set; }
        public int Total { get; set; }

        /// <summary>
        /// 轉換為StockDayHis格式
        /// 若當天停盤則遺棄該筆資料
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public IList<StockDayHistoryDto> ConvertToStockDayHisList(int stockId)
        {
            List<StockDayHistoryDto> result = new List<StockDayHistoryDto>(Total);
            foreach (var item in Data)
            {
                _ = long.TryParse(item[1].Replace(",", ""), out long tradeVolumn);
                _ = long.TryParse(item[2].Replace(",", ""), out long tradeValue);
                _ = double.TryParse(item[3], out double openingPrice);
                _ = double.TryParse(item[4], out double highestPrice);
                _ = double.TryParse(item[5], out double lowestPrice);
                _ = double.TryParse(item[6], out double closingPrice);
                _ = double.TryParse(item[7], out double change);
                _ = long.TryParse(item[8].Replace(",", ""), out long transaction);

                if (tradeVolumn != 0)
                {
                    result.Add(new StockDayHistoryDto()
                    {
                        DateTime = item[0].RocToAd(),
                        TradeVolumn = tradeVolumn,
                        TradeValue = tradeValue,
                        OpeningPrice = openingPrice,
                        HighestPrice = highestPrice,
                        LowestPrice = lowestPrice,
                        ClosingPrice = closingPrice,
                        Change = item[7].StartsWith("X") ? openingPrice - closingPrice : change,
                        Transaction = transaction,
                        StockId = stockId,
                    });
                }
            }

            return result;
        }
    }
}
