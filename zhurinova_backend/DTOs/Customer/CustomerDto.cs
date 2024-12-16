using zhurinova_backend.DTOs.Order;

namespace zhurinova_backend.DTOs.NewFolder
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<OrderDto> Orders { get; set; }
    }
}
