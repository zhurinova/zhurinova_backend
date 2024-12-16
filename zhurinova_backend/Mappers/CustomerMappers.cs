using zhurinova_backend.DTOs.Customer;
using zhurinova_backend.DTOs.NewFolder;
using zhurinova_backend.Model;

namespace zhurinova_backend.Mappers
{
    public static class CustomerMappers
    {
        public static CustomerDto ToCustomerDto(this Customer customerModel)
        {
            return new CustomerDto
            {
                Id = customerModel.Id,
                Name = customerModel.Name,
                Address = customerModel.Address,
                Email = customerModel.Email,
                Phone = customerModel.Phone,
                Orders = customerModel.Orders.Select(c => c.ToOrderDto()).ToList(),
            };
        }

        public static Customer ToCustomerFromCreateDto(this CreateCustomerRequestDto customerDto)
        {
            return new Customer
            {
                Name = customerDto.Name,
                Address = customerDto.Address,
                Email = customerDto.Email,
                Phone = customerDto.Phone
            };
        }
    }
}
