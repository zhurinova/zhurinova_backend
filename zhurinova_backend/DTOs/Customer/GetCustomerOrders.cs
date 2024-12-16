namespace zhurinova_backend.DTOs.Customer
{
    public class GetCustomerOrders
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
