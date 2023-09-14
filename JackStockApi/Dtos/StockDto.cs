namespace JackStockApi.Dtos
{
    public class StockDto
    {
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
        /// <summary>
        /// 最後更新日
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
    }
}
