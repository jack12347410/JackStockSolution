using JackLib;
using JackStock.Services;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JackStock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dateStr = "112/09/01";
            Console.WriteLine(dateStr.RocToAd());

            string volumn = "123,123";
            Console.WriteLine(Convert.ToInt32(volumn.Replace(",", "")));

            string a = "-0.20";
            Console.WriteLine(Convert.ToDouble(a));


            using (var http = new HttpClient())
            {
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "stockNo", "2353" },
                    { "date", DateTime.Now.ToString() }
                };
                var b = http.GetByParamAnsyc<TwseStockDayResponeDto>("http://www.twse.com.tw/exchangeReport/STOCK_DAY", param);
                b.Wait();
            }


            Console.ReadLine();



        }
    }

    public class TwseStockDayResponeDto
    {
        public string Stat { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string[] Fields { get; set; }
        public string[][] Data { get; set; }
        public string[] Notes { get; set; }
        public int Total { get; set; }
    }
}