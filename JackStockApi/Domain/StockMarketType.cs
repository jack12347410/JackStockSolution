using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackStockApi.Domain
{
    public class StockMarketType
    {
        /// <summary>
        /// 證券上市別ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 證券上市別名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 證券上市別代碼
        /// </summary>
        public string Code { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
