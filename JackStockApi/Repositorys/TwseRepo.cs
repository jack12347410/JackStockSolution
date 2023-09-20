using JackLib;
using JackStockApi.Dtos;

namespace JackStockApi.Repositorys
{
    public class TwseRepo
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TwseRepo(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 取得TWSE每日個股歷史資料
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public async Task<TwseStockDayResponeDto> GetTwseStockDayAsync(Dictionary<string, string> paras)
        {
            using(var http = _httpClientFactory.CreateClient())
            {
                return await http.GetByParamAnsyc<TwseStockDayResponeDto>(Str.TWSE_STOCK_DAY_URL, paras);
            }
        }
    }
}
