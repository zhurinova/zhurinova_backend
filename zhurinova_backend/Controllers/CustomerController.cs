using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zhurinova_backend.Data;
using zhurinova_backend.DTOs.Customer;
using zhurinova_backend.Helpers;
using zhurinova_backend.Interfaces;
using zhurinova_backend.Mappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zhurinova_backend.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ApplicationDBContext context, ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
            _context = context; 
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var customers = await _customerRepo.GetAllAsync(query);
            var customerDto = customers.Select(s => s.ToCustomerDto());

            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerRepo.GetByIdAsync(id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer.ToCustomerDto());
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequestDto customerDto)  // FromBody - потому что берем не с URL,а с HTTP 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerModel = customerDto.ToCustomerFromCreateDto();
            await _customerRepo.CreateAsync(customerModel);

            return CreatedAtAction(nameof(GetById), new { id = customerModel.Id }, customerModel.ToCustomerDto());
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCustomerRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerModel = await _customerRepo.UpdateAsync(id, updateDto);
            if(customerModel == null)
            {
                return NotFound();
            }
            return Ok(customerModel.ToCustomerDto());
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerModel = await _customerRepo.DeleteAsync(id);
            if (customerModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("amount-of-orders")]
        public async Task<IActionResult> GetCustomerOrders()
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var customersOrders = await _customerRepo.GetCustomerOrdersAsync();

            if (customersOrders == null)
            {
                return NotFound();
            }

            return Ok(customersOrders);
        }

        [HttpGet("with-average-price")]
        public async Task<IActionResult> GetCustomerAvrPriceOrder()
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var customersWithAvgPrice = await _customerRepo.GetCustomerAvrPriceOrderAsync();

            if (customersWithAvgPrice == null)
            {
                return NotFound();
            }

            return Ok(customersWithAvgPrice);
        }
    }
}
