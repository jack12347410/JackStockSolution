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
        private readonly StockContext _stockContext;

        public StockRepo(StockContext stockContext)
        {
            _stockContext = stockContext;
        }

        #region Stock

        public async Task<IList<Stock>> FindStockAllAsync()
        {
            return await _stockContext.Stock.AsQueryable().ToListAsync();
        }

        /// <summary>
        /// 取得stock by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Stock?> FindSockByIdAsync(int id)
        {
            return await _stockContext.Stock.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 取得stock by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<Stock?> FindSockByCodeAsync(string code)
        {
            return await _stockContext.Stock.AsQueryable().Where(x=>x.Code.Equals(code)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 取得stocks by marketType
        /// </summary>
        /// <param name="marketTypeId"></param>
        /// <returns></returns>
        public IQueryable<Stock> FindStocksByMarketTypeId(int? marketTypeId) 
        {
            var result = _stockContext.Stock.AsQueryable();
            if (marketTypeId.HasValue)
            {
                return result.Where(x => x.StockMarketTypeId == marketTypeId);
            }

            return result;
        }

        public async Task<int> InsertBatchStockAsync(IEnumerable<StockDto> dtos)
        {
            var sql = @"IF NOT EXISTS (SELECT [Id] FROM Stock 
                                        WHERE [Code] = @Code)
                        BEGIN
                            INSERT INTO Stock
		                        ([Name], [Code], [StockMarketTypeId])
                            VALUES (@Name, @Code, @StockMarketTypeId)
                        END";

            var paras = new List<DynamicParameters>();
            foreach (var d in dtos)
            {
                var param = new DynamicParameters();
                param.Add("@Name", d.Name);
                param.Add("@Code", d.Code);
                param.Add("@StockMarketTypeId", d.StockMarketTypeId);

                paras.Add(param);
            }

            return await _stockContext.Database.DapperTransactionAsync(sql, paras);
        }

        public async Task<int> UpdateStockAsync(Stock update)
        {
            _stockContext.Stock.Update(update);
            return await _stockContext.SaveChangesAsync();
        }
        #endregion Stock


        #region StockDayHistory

        public async Task<int> InsertStockDayHisAsync(StockDayHistoryDto dto)
        {
            var item = new StockDayHistory();
            _stockContext.StockDayHistory.Add(item).CurrentValues.SetValues(dto);
            return await _stockContext.SaveChangesAsync();
        }

        public async Task<int> InsertBatchStockDayHisAsync(IEnumerable<StockDayHistoryDto> dtos)
        {
            var sql = @"IF NOT EXISTS (SELECT [StockId] FROM StockDayHistory 
                                        WHERE [StockId] = @StockId 
                                        AND [DateTime] = @DateTime)
                        BEGIN
                            INSERT INTO StockDayHistory
		                        ([StockId],[DateTime],[TradeVolumn],[TradeValue],[OpeningPrice],[HighestPrice],
		                        [LowestPrice],[ClosingPrice],[Change],[Transaction])
                            VALUES (@StockId, @DateTime, @TradeVolumn, @TradeValue, @OpeningPrice, @HighestPrice,
		                        @LowestPrice, @ClosingPrice, @Change, @Transaction)

                            UPDATE STOCK
                            SET [LastUpdateDate] = @DateTime
                            WHERE [Id] = @StockId
                        END";

            var paras = new List<DynamicParameters>(dtos.Count());
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

            return await _stockContext.Database.DapperTransactionAsync(sql, paras);
        }

        #endregion StockDayHistory
    }
}
