namespace zhurinova_backend.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; }
    }
}
