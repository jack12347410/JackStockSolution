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

        public async Task<IList<Stock>> FindStockAllAsync()
        {
            return await _stockRepo.FindStockAllAsync();
        }

        public async Task<Stock?> FindStockByIdAsync(int id)
        {
            return await _stockRepo.FindSockByIdAsync(id);
        }

        public async Task<Stock?> FindStockByCodeAsync(string stockCode)
        {
            return await _stockRepo.FindSockByCodeAsync(stockCode);
        }

        public async Task<IList<Stock>> FindStockByMarketTypeIdAsync(int? marketTypeId)
        {
            var result = _stockRepo.FindStocksByMarketTypeId(marketTypeId);

            return await result.OrderBy(x => x.Code).ToListAsync();
        }

        public async Task<int> InsertBatchStockDayHisAsync(IEnumerable<StockDayHistoryDto> dtos)
        {
            return await _stockRepo.InsertBatchStockDayHisAsync(dtos);
        }

        public async Task<int> InsertBatchStockAsync(IEnumerable<StockDto> dtos)
        {
            return await _stockRepo.InsertBatchStockAsync(dtos);
        }

        public async Task<int> UpdateStockLastUpdateDate(int stockId, DateTime newDate)
        {
            var update = await FindStockByIdAsync(stockId);
            if(update != null)
            {
                update.LastUpdateDate = newDate;
                return await _stockRepo.UpdateStockAsync(update);
            }

            return 0;
        }
    }
}
