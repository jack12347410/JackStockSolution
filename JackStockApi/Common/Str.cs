namespace JackStockApi
{
    internal static class Str
    {
        /// <summary>
        /// 台股股市base api
        /// </summary>
        public static string TWSE_BASE_URL = "http://www.twse.com.tw/exchangeReport/";

        /// <summary>
        /// 個股日成交資訊api
        /// </summary>
        public static string TWSE_STOCK_DAY_URL = $"{TWSE_BASE_URL}STOCK_DAY";
    }
}
