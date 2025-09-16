using Dapper;
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

        public async Task<Transaction?> FindByIdAsync(int id)
        {
            var sql = @"
                select 
                    id as Id,
                    fromaccountid as FromAccountId,
                    toaccountid as ToAccountId,
                    amount as Amount,
                    performedat as PerformedAt,
                    status as Status
                from transactions
                where id = @id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Transaction>(sql, new { id });
        }

        public async Task<IEnumerable<Transaction>> FindAllAsync()
        {
            var sql = @"
                select 
                    id as Id,
                    fromaccountid as FromAccountId,
                    toaccountid as ToAccountId,
                    amount as Amount,
                    performedat as PerformedAt,
                    status as Status
                from transactions";

            return await _dbConnection.QueryAsync<Transaction>(sql);
        }

        public async Task<IEnumerable<Transaction>> SearchAsync(
            int? customer_id,
            DateTime? date_from,
            DateTime? date_to,
            decimal? min_amount,
            decimal? max_amount,
            string? status)
        {
            var sql = @"
                select
                    t.id as Id,
                    t.fromaccountid as FromAccountId,
                    t.toaccountid as ToAccountId,
                    t.amount as Amount,
                    t.performedat as PerformedAt,
                    t.status as Status
                from transactions t
                join accounts a1 on a1.id = t.fromaccountid 
                join customers c on c.id = a1.customerid 
                where 1 = 1";

            var parameters = new DynamicParameters();

            if (customer_id != null)
            {
                sql += " and c.id = @CustomerId";
                parameters.Add("CustomerId", customer_id);
            }

            if (date_from != null)
            {
                sql += " and t.performedat >= @DateFrom";
                parameters.Add("DateFrom", date_from);
            }

            if (date_to != null)
            {
                sql += " and t.performedat <= @DateTo";
                parameters.Add("DateTo", date_to);
            }

            if (min_amount != null)
            {
                sql += " and t.amount >= @MinAmount";
                parameters.Add("MinAmount", min_amount);
            }

            if (max_amount != null)
            {
                sql += " and t.amount <= @MaxAmount";
                parameters.Add("MaxAmount", max_amount);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql += " and t.status = @Status";
                parameters.Add("Status", status);
            }

            sql += " order by t.id";

            return await _dbConnection.QueryAsync<Transaction>(sql, parameters);
        }

        public async Task<IEnumerable<Transaction>> GetSuspiciousTransactionsAsync()
        {
            var sql = @"
                select 
                    id as Id,
                    fromaccountid as FromAccountId,
                    toaccountid as ToAccountId,
                    amount as Amount,
                    performedat as PerformedAt,
                    status as Status
                from transactions
                where amount > 10000

                union

                select 
                    t.id as Id,
                    t.fromaccountid as FromAccountId,
                    t.toaccountid as ToAccountId,
                    t.amount as Amount,
                    t.performedat as PerformedAt,
                    t.status as Status
                from transactions t
                join accounts a on a.id = t.fromaccountid
                where (
                    select count(*)
                    from transactions t2
                    where t2.fromaccountid = a.id AND 
                          t2.performedat >= now() - interval '10 minutes'
                ) > 5;";

            return await _dbConnection.QueryAsync<Transaction>(sql);
        }

        public async Task<int> CreateAsync(Transaction transaction)
        {
            var sql = @"
                insert into transactions (fromaccountid, toaccountid, amount, performedat, status)
                values 
                    (@FromAccountId, @ToAccountId, @Amount, @PerformedAt, @Status)
                returning id;";

            return await _dbConnection.ExecuteScalarAsync<int>(sql, transaction);
        }

        public async Task<int> UpdateAsync(Transaction transaction)
        {
            var sql = @"
                update transactions
                set 
                    status = @Status
                where id = @Id;";

            return await _dbConnection.ExecuteAsync(sql, transaction);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"
                delete
                from transactions
                where id = @id";

            return await _dbConnection.ExecuteAsync(sql, new { id });
        }
    }
}
