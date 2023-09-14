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
        private readonly StockRepo _stockRepo;

        public StockService(StockRepo stockRepo) 
        {
            _stockRepo = stockRepo;
        }

        public async Task<Stock> FindStockByCodeAsync(string stockCode)
        {
            return await _stockRepo.FindSockByCodeAsync(stockCode);
        }

        public async Task<IList<Stock>> FindStockByMarketTypeIdAsync(int? marketTypeId)
        {
            var result = _stockRepo.FindStocksByMarketTypeId(marketTypeId);

            return await result.OrderBy(x => x.Code).ToListAsync();
        }

        public Task<int> InsertBatchStockDayHisAsync(IEnumerable<StockDayHistoryDto> dtos)
        {
            return _stockRepo.InsertBatchStockDayHisAsync(dtos);
        }


        public Task<int> InsertBatchStockAsync(IEnumerable<StockDto> dtos)
        {
            return _stockRepo.InsertBatchStockAsync(dtos);
        }
    }
}
