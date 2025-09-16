using Exam.Application.Repositories;
using Exam.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var customers = await _customerRepository.FindAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var customer = await _customerRepository.FindByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            var id = await _customerRepository.CreateAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Customer customer)
        {
            if (id != customer.Id) return BadRequest();

            var updated = await _customerRepository.UpdateAsync(customer);
            if (updated == 0) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _customerRepository.DeleteAsync(id);
            if (deleted == 0) return NotFound();

            return NoContent();
        }
    }
}
