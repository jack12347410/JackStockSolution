namespace JackStockApi.Dtos
{
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
