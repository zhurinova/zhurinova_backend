using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using zhurinova_backend.Data;
using zhurinova_backend.Interfaces;
using zhurinova_backend.Model;

namespace zhurinova_backend.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order orderModel)
        {
            await _context.Orders.AddAsync(orderModel);
            await _context.SaveChangesAsync();
            return orderModel;
        }

        public async Task<Order?> DeleteAsync(int id)
        {
            var orderMidel = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (orderMidel == null)
            {
                return null;
            }

            _context.Orders.Remove(orderMidel);
            await _context.SaveChangesAsync();  
            return orderMidel;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order?> UpdateAsync(int id, Order orderModel)
        {
            var existingOrder = await _context.Orders.FindAsync(id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.Status = orderModel.Status;
            existingOrder.DateTime = orderModel.DateTime;

            await _context.SaveChangesAsync();

            return existingOrder;
        }
    }
}
