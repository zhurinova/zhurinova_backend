using System.ComponentModel.DataAnnotations;

namespace zhurinova_backend.DTOs.Customer
{
    public class UpdateCustomerRequestDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(10)]
        [MaxLength(13)]
        public string Phone { get; set; } = string.Empty;
    }
}
