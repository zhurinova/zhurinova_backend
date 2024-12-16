namespace zhurinova_backend.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
