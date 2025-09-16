using Exam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Application.Repositories
{
    public interface IAccountRepository
    {
        Task<Account>? FindByIdAsync(int id);
        Task<IEnumerable<Account>> FindAllAsync();
        Task<int> CreateAsync(Account account);
        Task<int> UpdateAsync(Account account);
        Task<int> DeleteAsync(int id);
    }
}
