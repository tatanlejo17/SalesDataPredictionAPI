namespace SalesDataPredictionAPI.Models
{
    public class Customer
    {
        public string CompanyName { get; set; } = "";
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}