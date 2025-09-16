using Exam.Application.DTOs;

namespace Exam.Application.Repositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<TopCustomerDto>> GetTopCustomersAsync(int limit = 5);
        Task<decimal> GetBankIncomeAsync(DateTime? date_from, DateTime? date_to);
    }
}
