using Microsoft.EntityFrameworkCore;
using zhurinova_backend.Data;
using zhurinova_backend.DTOs.Customer;
using zhurinova_backend.Helpers;
using zhurinova_backend.Interfaces;
using zhurinova_backend.Mappers;
using zhurinova_backend.Model;

namespace zhurinova_backend.Repository
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly ApplicationDBContext _context;
        public CustomerRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Customer> CreateAsync(Customer customerModel)
        {
            await _context.Customers.AddAsync(customerModel);
            await _context.SaveChangesAsync();
            return customerModel;
        }

        public Task<bool> CustomerExist(int id)
        {
            return _context.Customers.AnyAsync(s => s.Id == id);
        }

        public async Task<Customer?> DeleteAsync(int id)
        {
            var customerModel = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customerModel == null)
            {
                return null;
            }
            _context.Customers.Remove(customerModel);

            await _context.SaveChangesAsync();
            return customerModel;
        }

        public async Task<List<Customer>> GetAllAsync(QueryObject query)
        {
            var customers = _context.Customers.Include(c => c.Orders).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Name))
            {
                customers = customers.Where(s => s.Name.Contains(query.Name));
            }
            if(!string.IsNullOrWhiteSpace(query.Address))
            {
                customers = customers.Where(s => s.Address.Contains(query.Address));
            }
            return await customers.ToListAsync();

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    customers = query.IsDecsending ? customers.OrderByDescending(s => s.Name) : customers.OrderBy(s => s.Name);
                }
            }
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.Include(c => c.Orders).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Customer?> UpdateAsync(int id, UpdateCustomerRequestDto customerDto)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer == null)
            {
                return null;
            }

            existingCustomer.Name = customerDto.Name;
            existingCustomer.Address = customerDto.Address;
            existingCustomer.Email = customerDto.Email;
            existingCustomer.Phone = customerDto.Phone;

            await _context.SaveChangesAsync();

            return existingCustomer;
        }


        public async Task<List<GetCustomerOrders>> GetCustomerOrdersAsync()
        {
            var customerWithOrders = from customer in _context.Customers
                                     join order in _context.Orders on customer.Id equals order.CustomerId into groupedOrders
                                     select new GetCustomerOrders
                                     {
                                         Id = customer.Id,
                                         Name = customer.Name,
                                         Email = customer.Email,
                                         Count = groupedOrders.Count(),
                                     };
            return await customerWithOrders.ToListAsync();
        }
        public async Task<List<CustomerWithAvgPriceOrderDto>> GetCustomerAvrPriceOrderAsync()
        {
            var customerWithAvgOrderPrice = from customer in _context.Customers
                                            select new CustomerWithAvgPriceOrderDto
                                            {
                                                Id = customer.Id,
                                                Name = customer.Name,
                                                AvrPriceOrder = customer.Orders.Any()
                                                    ? customer.Orders.Average(e => (decimal?)e.Price) ?? 0
                                                    : 0,
                                                NumberOfOrders = customer.Orders.Count()
                                            };
            return await customerWithAvgOrderPrice.ToListAsync();
        }
    }
}
