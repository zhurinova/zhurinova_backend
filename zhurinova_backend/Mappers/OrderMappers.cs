using zhurinova_backend.DTOs.Order;
using zhurinova_backend.Model;

namespace zhurinova_backend.Mappers
{
    public static class OrderMappers
    {
        public static OrderDto ToOrderDto(this Order orderModel)
        {
            return new OrderDto
            {
                Id = orderModel.Id,
                Status = orderModel.Status,
                DateTime = orderModel.DateTime,
                CustomerId = orderModel.CustomerId
            };
         }

        public static Order ToOrderFromCreate(this CreateOrderRequestDto orderDto, int customerId)
        {
            return new Order
            {
                Status = orderDto.Status,
                DateTime = orderDto.DateTime,
                CustomerId = customerId
            };
        }
        public static Order ToOrderFromUpdate(this UpdateOrderRequestDto orderDto)
        {
            return new Order
            {
                Status = orderDto.Status,
                DateTime = orderDto.DateTime,
            };
        }
    }
}
