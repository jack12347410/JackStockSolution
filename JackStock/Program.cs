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
            //Console.WriteLine(DateTime.Now.ToString("yyyyMMdd"));

            string dateStr = " 99/01/01";
            Console.WriteLine(dateStr.RocToAd());

            DateTime dateOnly = new DateTime(2022, 9, 30);
            DateTime dateTime = DateTime.Now.Date;
            Console.WriteLine(dateOnly);
            Console.WriteLine(dateTime);
            Console.WriteLine(dateOnly.AddDays(1));
            Console.WriteLine(new DateOnly(2023, 4, 1).ToString("yyyyMMdd"));

            //string volumn = "123,123";
            //Console.WriteLine(Convert.ToInt32(volumn.Replace(",", "")));

            //string a = "-0.20";
            //Console.WriteLine(Convert.ToDouble(a));


            //using (var http = new HttpClient())
            //{
            //    Dictionary<string, string> param = new Dictionary<string, string>
            //    {
            //        { "stockNo", "2353" },
            //        { "date", DateTime.Now.ToString() }
            //    };
            //    var b = http.GetByParamAnsyc<TwseStockDayResponeDto>("http://www.twse.com.tw/exchangeReport/STOCK_DAY", param);
            //    b.Wait();
            //}

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