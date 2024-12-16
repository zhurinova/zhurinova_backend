namespace zhurinova_backend.DTOs.Customer
{
    public class CustomerWithAvgPriceOrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal AvrPriceOrder { get; set; }
        public int NumberOfOrders {  get; set; }
    }
}
