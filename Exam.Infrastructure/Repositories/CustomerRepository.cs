using Dapper;
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

        public async Task<Customer?> FindByIdAsync(int id)
        {
            var sql = @"
                select
                    id as Id,
                    fullname as FullName,   
                    phone as Phone,
                    email as Email,
                    registeredat as RegisteredAt,
                    isactive as IsActive
                from customers
                where id = @id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { id });
        }

        public async Task<IEnumerable<Customer>> FindAllAsync()
        {
            var sql = @"
                select
                    id as Id,
                    fullname as FullName,   
                    phone as Phone,
                    email as Email,
                    registeredat as RegisteredAt,
                    isactive as IsActive
                from customers";

            return await _dbConnection.QueryAsync<Customer>(sql);
        }

        public async Task<int> CreateAsync(Customer customer)
        {
            var sql = @"
                insert into customers (fullname, phone, email, registeredat)
                values 
                    (@FullName, @Phone, @Email, @RegisteredAt)
                returning id;";

            return await _dbConnection.ExecuteScalarAsync<int>(sql, customer);
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            var sql = @"
                update customers
                set fullname = @FullName,
                    phone = @Phone,
                    email = @Email,
                    isactive = @IsActive
                where id = @Id;";

            return await _dbConnection.ExecuteAsync(sql, customer);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"
                delete
                from customers
                where id = @id";

            return await _dbConnection.ExecuteAsync(sql, new { id });
        }
    }
}
