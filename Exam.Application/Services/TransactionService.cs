using Exam.Application.Repositories;
using Exam.Domain.Entities;

namespace Exam.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;

        public TransactionService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task CreateTransactionAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            var transaction = new Transaction()
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                Amount = amount

            };
            var accauntFrom = await _accountRepository.FindByIdAsync(fromAccountId);
            var accauntTo = await _accountRepository.FindByIdAsync(toAccountId);

            if (accauntFrom == null || accauntTo == null)
            {
                transaction.Status = "Failed";
                await _transactionRepository.CreateAsync(transaction);
                return;
            }

            if (accauntFrom.Balance < amount * 1.01m)
            {
                transaction.Status = "Failed";
                await _transactionRepository.CreateAsync(transaction);
                return;
            }

            accauntFrom.Balance -= amount * 1.01m;
            accauntTo.Balance += amount;

            await _accountRepository.UpdateAsync(accauntFrom);
            await _accountRepository.UpdateAsync(accauntTo);

            transaction.Status = "Success";

            await _transactionRepository.CreateAsync(transaction);
        }
    }
}
