using Exam.Application.Repositories;
using Exam.Domain.Entities;
using System.Data;

namespace Exam.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbConnection _dbConnection;

        public TransactionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Transaction>? FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> SearchAsync(
            int? customer_id,
            DateTime? date_from,
            DateTime? date_to,
            decimal? min_amount,
            decimal? max_amount,
            string? status)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
