using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using zhurinova_backend.DTOs.Order;
using zhurinova_backend.Interfaces;
using zhurinova_backend.Mappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zhurinova_backend.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICustomerRepository _customerRepo;
        public OrderController(IOrderRepository orderRepo, ICustomerRepository customerRepo)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepo.GetAllAsync();
            var orderDto = orders.Select(s => s.ToOrderDto());
            return Ok(orderDto);
        }

        // GET api/<OrderController>/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order.ToOrderDto());
        }

        // POST api/<OrderController>
        [HttpPost]
        [Route("{customerId:int}")]
        public async Task<IActionResult> Create([FromRoute] int customerId, CreateOrderRequestDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _customerRepo.CustomerExist(customerId))
            {
                return BadRequest("Customer does not exist");
            }
            var orderModel = orderDto.ToOrderFromCreate(customerId);
            await _orderRepo.CreateAsync(orderModel);
            return CreatedAtAction(nameof(GetById), new { id = orderModel.Id }, orderModel.ToOrderDto());
        }

        // PUT api/<OrderController>/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderRepo.UpdateAsync(id, updateDto.ToOrderFromUpdate());

            if (order == null)
            {
                return NotFound("Order not found");
            }
            return Ok(order.ToOrderDto());
        }

        //DELETE api/<OrderController>/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderModel = await _orderRepo.DeleteAsync(id);
            if (orderModel == null)
            {
                return NotFound("Order does not exist");
            }

            return Ok(orderModel);

        }
    }
}
