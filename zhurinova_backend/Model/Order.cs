namespace zhurinova_backend.Model
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
