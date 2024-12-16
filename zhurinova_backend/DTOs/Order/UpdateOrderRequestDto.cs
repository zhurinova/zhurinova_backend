using System.ComponentModel.DataAnnotations;

namespace zhurinova_backend.DTOs.Order
{
    public class UpdateOrderRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be Opened/Closed")]
        [MaxLength(7, ErrorMessage = "Title must be Opened/Closed")]
        public string? Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
