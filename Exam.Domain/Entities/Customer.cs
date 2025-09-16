using Exam.Domain.Common;

namespace Exam.Domain.Entities
{
    public class Customer : Entity
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }
        public bool IsActive { get; set; }
    }
}
