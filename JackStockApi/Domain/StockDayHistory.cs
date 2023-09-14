using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackStockApi.Domain
{
    public class StockDayHistory
    {
        /// <summary>
        /// 證券ID
        /// </summary>
        public int StockId { get; set; }
        /// <summary>
        /// 成交日期
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 成交股數
        /// </summary>
        public long TradeVolumn { get; set; }
        /// <summary>
        /// 成交金額 
        /// </summary>
        public long TradeValue { get; set; }
        /// <summary>
        /// 開盤價
        /// </summary>
        public double OpeningPrice { get; set; }
        /// <summary>
        /// 最高價
        /// </summary>
        public double HighestPrice { get; set; }
        /// <summary>
        /// 最低價
        /// </summary>
        public double LowestPrice { get; set; }
        /// <summary>
        /// 收盤價
        /// </summary>
        public double ClosingPrice { get; set;}
        /// <summary>
        /// 漲跌價差
        /// </summary>
        public double Change { get; set; }
        /// <summary>
        /// 成交筆數
        /// </summary>
        public long Transaction { get; set; }


        public virtual Stock Stock { get; set; } 
    }
}
