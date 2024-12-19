
using System.ComponentModel.DataAnnotations;

namespace zhurinova_backend.DTOs.Order
{
    public class CreateOrderRequestDto
    {
        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Title must be Opened/Closed")]
        [MaxLength(7, ErrorMessage = "Title must be Opened/Closed")]
        public string? Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
