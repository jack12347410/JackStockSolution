using Dapper;
using JackLib;
using JackStockApi.Data;
using JackStockApi.Domain;
using JackStockApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JackStockApi.Repositorys
{
    public class StockRepo
    {
        private readonly StockContext _context;

        public StockRepo(StockContext context)
        {
            _context = context;
        }

        //public Task<int> Get()
        //{
        //}

        public Task<int> InsertAsync(StockDayHistoryDto dto)
        {
            //_context.Database.GetDbConnection();

            var item = new StockDayHistory();
            _context.StockDayHistory.Add(item).CurrentValues.SetValues(dto);
            return _context.SaveChangesAsync();
        }

        public Task InsertBatchAsync(IEnumerable<StockDayHistoryDto> dtos)
        {
            var sql = @"IF NOT EXISTS (SELECT [StockId] FROM StockDayHistory 
                                        WHERE [StockId] = @StockId 
                                        AND [DateTime] = @DateTime)
                        BEGIN
                            INSERT INTO StockDayHistory
		                        ([StockId],[DateTime],[TradeVolumn],[TradeValue],[OpeningPrice],[HighestPrice],
		                        [LowestPrice],[ClosingPrice],[Change],[Transaction])
                            VALUES (@StockId, @DataTime, @TradeVolumn, @TradeValue, @OpeningPrice, @HighestPrice,
		                        @LowestPrice, @ClosingPrice, @Change, @Transaction)
                        END";

            var paras = new List<DynamicParameters>();
            foreach (var d in dtos)
            {
                var param = new DynamicParameters();
                param.Add("@StockId", d.StockId);
                param.Add("@DateTime", d.DateTime);
                param.Add("@TradeVolumn", d.TradeVolumn);
                param.Add("@TradeValue", d.TradeValue);
                param.Add("@OpeningPrice", d.OpeningPrice);
                param.Add("@HighestPrice", d.HighestPrice);
                param.Add("@LowestPrice", d.LowestPrice);
                param.Add("@ClosingPrice", d.ClosingPrice);
                param.Add("@Change", d.Change);
                param.Add("@Transaction", d.Transaction);

                paras.Add(param);
            }

            return _context.Database.DapperTransactionAsync(sql, paras);
        }
    }
}
