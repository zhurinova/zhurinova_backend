using System.ComponentModel.DataAnnotations.Schema;

namespace zhurinova_backend.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; }
    }
}
