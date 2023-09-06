using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackStockApi.Domain
{
    public class Stock
    {
        /// <summary>
        /// 證券ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 證券名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 證券代碼
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 證券上市別
        /// </summary>
        public int StockMarketTypeId { get; set; }

        public virtual StockMarketType StockMarketType { get; set; }
        public virtual ICollection<StockDayHistory> StockHistories { get; set; }
    }
}
