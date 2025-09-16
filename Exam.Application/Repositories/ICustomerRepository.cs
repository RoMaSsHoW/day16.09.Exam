using Exam.Domain.Entities;

namespace Exam.Application.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer>? FindByIdAsync(int id);
        Task<IEnumerable<Customer>> FindAllAsync();
        Task<int> CreateAsync(Customer customer);
        Task<int> UpdateAsync(Customer customer);
        Task<int> DeleteAsync(int id);
    }
}
