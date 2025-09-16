using Exam.Domain.Entities;

namespace Exam.Application.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction?> FindByIdAsync(int id);
        Task<IEnumerable<Transaction>> FindAllAsync();
        Task<IEnumerable<Transaction>> SearchAsync(
            int? customer_id,
            DateTime? date_from,
            DateTime? date_to,
            decimal? min_amount,
            decimal? max_amount,
            string? status);
        Task<int> CreateAsync(Transaction transaction);
        Task<int> UpdateAsync(Transaction transaction);
        Task<int> DeleteAsync(int id);
    }
}
