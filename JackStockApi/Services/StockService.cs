using JackLib;
using JackStockApi.Data;
using JackStockApi.Domain;
using JackStockApi.Dtos;
using JackStockApi.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace JackStockApi.Services
{
    public class StockService
    {
        private readonly StockRepo _repo;

        public StockService(StockRepo repo) 
        {
            _repo = repo;
        }

        public async Task<IList<Stock>> FindStockByMarketTypeIdAsync(int? marketTypeId)
        {
            var result = _repo.FindStocksByMarketTypeId(marketTypeId);

            return await result.OrderBy(x => x.Code).ToListAsync();
        }

        public Task<int> InsertBatchStockDayHisAsync(IEnumerable<StockDayHistoryDto> dtos)
        {
            //List<StockDayHistoryDto> dtos = new List<StockDayHistoryDto>();
            //stockDayHistories.ForEach(x=>
            //{
            //    var data = new StockDayHistoryDto()
            //    {
            //        DateTime = x.DateTime,
            //        StockId = x.StockId,
            //        Change = x.Change,
            //        ClosingPrice = x.ClosingPrice,
            //        HighestPrice = x.HighestPrice,
            //        LowestPrice = x.LowestPrice,
            //        OpeningPrice = x.OpeningPrice,
            //        TradeValue = x.TradeValue,
            //        TradeVolumn = x.TradeVolumn,
            //        Transaction = x.Transaction,
            //    };

            //    dtos.Add(data);
            //});
            return _repo.InsertBatchStockDayHisAsync(dtos);
        }


        public Task<int> InsertBatchStockAsync(IEnumerable<StockDto> dtos)
        {
            //List<StockDto> dtos = new List<StockDto>();
            //stocks.ForEach(x =>
            //{
            //    var data = new StockDto()
            //    {
            //        StockMarketTypeId = x.StockMarketTypeId,    
            //        Code = x.Code,  
            //        Name = x.Name,  
            //    };

            //    dtos.Add(data);
            //});

            return _repo.InsertBatchStockAsync(dtos);
        }
    }
}
