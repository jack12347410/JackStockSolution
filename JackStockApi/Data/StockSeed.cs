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
    }
}
