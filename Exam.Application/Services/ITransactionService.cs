namespace Exam.Application.Services
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(int fromAccountId, int toAccountId, decimal amount);
    }
}
