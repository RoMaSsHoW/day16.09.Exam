using Exam.Application.Repositories;
using Exam.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            var accounts = await _accountRepository.FindAllAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            var account = await _accountRepository.FindByIdAsync(id);
            if (account == null)
                return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Account account)
        {
            var id = await _accountRepository.CreateAsync(new Account(account.CustomerId, account.Balance, account.Currency));
            return CreatedAtAction(nameof(GetById), new { id }, account);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Account account)
        {
            if (id != account.Id) return BadRequest();

            var updated = await _accountRepository.UpdateAsync(account);
            if (updated == 0) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _accountRepository.DeleteAsync(id);
            if (deleted == 0) return NotFound();

            return NoContent();
        }
    }
}
