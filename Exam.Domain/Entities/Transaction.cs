using Exam.Domain.Common;

namespace Exam.Domain.Entities
{
    public class Transaction : Entity
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
    }
}
