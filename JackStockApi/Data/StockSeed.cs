using JackStockApi.Domain;

namespace JackStockApi.Data
{
    internal class StockSeed
    {
        public static async Task SeedAsync(StockContext context)
        {
            if(!context.StockMarketType.Any())
            {
                context.StockMarketType.AddRange(GetDefaultStockMarketTypes());
                await context.SaveChangesAsync();
            }
        }

        public static IEnumerable<StockMarketType> GetDefaultStockMarketTypes()
        {
            return new List<StockMarketType>()
            {
                new StockMarketType(){Name = "上市", Code = "tse"},
                new StockMarketType(){Name = "上櫃", Code = "otc"}
            };
        }

        //public static IEnumerable<Stock> GetDefaultStocks(StockContext context)
        //{
        //    var tseId = context.StockMarketType.Where(x => x.Code.Equals("tse")).Select(x => x.Id).First();
        //    var otcId = context.StockMarketType.Where(x => x.Code.Equals("otc")).Select(x => x.Id).First();
        //    return new List<Stock>()
        //    {
        //        new Stock(){Name = "宏碁", Code = "2353", StockMarketTypeId = tseId},
        //        new Stock(){Name = "台積電", Code = "2330", StockMarketTypeId = otcId}
        //    };
        //}

        //public static IEnumerable<StockDayHistory> GetStockDayHistories(StockContext context)
        //{
        //    var a = context.Stock.Where(x => x.Code.Equals("2353")).Select(x => x.Id).First();
        //    var b = context.Stock.Where(x => x.Code.Equals("2330")).Select(x => x.Id).First();
        //    return new List<StockDayHistory>()
        //    {
        //        new StockDayHistory(){StockId = a, DateTime = ("2023/09/01").}
        //    }
        //}
    }
}
