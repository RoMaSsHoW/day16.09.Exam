using Exam.Application.Repositories;
using Exam.Domain.Entities;
using System.Data;

namespace Exam.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<Customer>? FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
