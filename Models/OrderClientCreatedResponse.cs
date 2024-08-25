namespace SalesDataPredictionAPI.Models
{
    public class OrderClientCreatedResponse
    {
        public int OrderId { get; set; }
        public NewOrderClient newOrderClient { get; set; } = new NewOrderClient();
    }
}