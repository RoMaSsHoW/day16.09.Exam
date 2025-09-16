using Exam.Application.Repositories;
using Exam.Application.Services;
using Exam.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionService _transactionService;

        public TransactionsController(
            ITransactionRepository transactionRepository,
            ITransactionService transactionService)
        {
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
        {
            var transactions = await _transactionRepository.FindAllAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetById(int id)
        {
            var transaction = await _transactionRepository.FindByIdAsync(id);
            if (transaction == null)
                return NotFound();
            return Ok(transaction);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Transaction>>> Search(
            [FromQuery] int? customer_id,
            [FromQuery] DateTime? date_from,
            [FromQuery] DateTime? date_to,
            [FromQuery] decimal? min_amount,
            [FromQuery] decimal? max_amount,
            [FromQuery] string? status)
        {
            var transactions = await _transactionRepository.SearchAsync(
                customer_id, date_from, date_to, min_amount, max_amount, status);

            return Ok(transactions);
        }

        [HttpGet("suspicious")]
        public async Task<IActionResult> GetSuspicious()
        {
            var result = await _transactionRepository.GetSuspiciousTransactionsAsync();
            return Ok(result);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            await _transactionService.CreateTransactionAsync(fromAccountId, toAccountId, amount);
            return Ok("Transaction processed");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Transaction transaction)
        {
            if (id != transaction.Id) return BadRequest();

            var updated = await _transactionRepository.UpdateAsync(transaction);
            if (updated == 0) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _transactionRepository.DeleteAsync(id);
            if (deleted == 0) return NotFound();

            return NoContent();
        }
    }
}
