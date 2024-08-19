namespace Redis_Sample.Models
{
    [Serializable]
    public class DashBoardData
    {
        public int TotalCustomerCount { get; set; }
        public int TotalRevenue { get; set; }
        public string TopSelllingProductName { get; set; }
        public string TopSelllingCountryName { get; set; }
    }
}
