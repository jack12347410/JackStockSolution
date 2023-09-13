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
            return _repo.InsertBatchStockDayHisAsync(dtos);
        }


        public Task<int> InsertBatchStockAsync(IEnumerable<StockDto> dtos)
        {
            return _repo.InsertBatchStockAsync(dtos);
        }
    }
}
