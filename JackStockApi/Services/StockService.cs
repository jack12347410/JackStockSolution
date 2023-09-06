using JackStockApi.Data;
using JackStockApi.Domain;
using JackStockApi.Dtos;
using JackStockApi.Repositorys;

namespace JackStockApi.Services
{
    public class StockService
    {
        private readonly StockRepo _repo;

        public StockService(StockRepo repo) 
        {
            _repo = repo;
        }

        public Task<int> InsertAsync(StockDayHistoryDto dto)
        {
            return _repo.InsertAsync(dto);
        }

        public Task<int> InsertAsync(IList<StockDayHistoryDto> dtos)
        {
            return _repo.InsertAsync(dtos);
        }
    }
}
