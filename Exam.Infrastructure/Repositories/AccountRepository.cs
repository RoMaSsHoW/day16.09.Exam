using Dapper;
using Exam.Application.Repositories;
using Exam.Domain.Entities;
using System.Data;

namespace Exam.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _dbConnection;

        public AccountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Account?> FindByIdAsync(int id)
        {
            var sql = @"
                select * 
                from accounts 
                where id = @id";

            return await _dbConnection.QuerySingleOrDefaultAsync<Account>(sql, new { id });
        }

        public async Task<IEnumerable<Account>> FindAllAsync()
        {
            var sql = @"
                select *
                from accounts";

            return await _dbConnection.QueryAsync<Account>(sql);
        }

        public async Task<int> CreateAsync(Account account)
        {
            var sql = @"
                insert into accounts (customerid, accountnumber, balance, currency)
                values 
                    (@customerid, @accountnumber, @balance, @currency)
                returning id;";

            return await _dbConnection.ExecuteScalarAsync<int>(sql, account);
        }

        public async Task<int> UpdateAsync(Account account)
        {
            var sql = @"
                update accounts
                set balance = @balance,
                    currency = @currency
                where id = @id;";

            return await _dbConnection.ExecuteAsync(sql, account);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"
                delete
                from accounts
                where id = @id";

            return await _dbConnection.ExecuteAsync(sql, new { id });
        }
    }
}
