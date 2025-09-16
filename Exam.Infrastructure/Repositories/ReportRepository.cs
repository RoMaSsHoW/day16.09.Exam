using Dapper;
using Exam.Application.DTOs;
using Exam.Application.Repositories;
using System.Data;

namespace Exam.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDbConnection _dbConnection;

        public ReportRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<TopCustomerDto>> GetTopCustomersAsync(int limit = 5)
        {
            var sql = @"
                select
                    c.fullname as FullName,
                    coalesce(sum(t.amount), 0) as TotalTurnover
                from customers c
                join accounts a on c.id = a.customerid
                join transactions t on t.fromaccountid = a.id
                                    or t.toaccountid = a.id
                where t.status = 'Success'
                group by c.fullname
                order by TotalTurnover desc
                limit @limit";

            return await _dbConnection.QueryAsync<TopCustomerDto>(sql, new { limit });
        }

        public async Task<decimal> GetBankIncomeAsync(DateTime? date_from, DateTime? date_to)
        {
            var sql = @"
                select coalesce(sum(t.amount * 0.01), 0)
                from transactions t
                where t.status = 'Success'";

            var parameters = new DynamicParameters();

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

            return await _dbConnection.ExecuteScalarAsync<decimal>(sql, parameters);
        }
    }
}
