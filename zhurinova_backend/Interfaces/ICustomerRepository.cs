using zhurinova_backend.DTOs.Customer;
using zhurinova_backend.Helpers;
using zhurinova_backend.Model;

namespace zhurinova_backend.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync(QueryObject query);
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer> CreateAsync(Customer customerModel);
        Task<Customer?> UpdateAsync(int id, UpdateCustomerRequestDto customerDto);
        Task<Customer?> DeleteAsync(int id);
        Task<bool> CustomerExist(int id);
        Task<List<GetCustomerOrders>> GetCustomerOrdersAsync();
        Task<List<CustomerWithAvgPriceOrderDto>> GetCustomerAvrPriceOrderAsync();

    }
}
