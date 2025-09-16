using Exam.Domain.Common;
using System.Text;

namespace Exam.Domain.Entities
{
    public class Account : Entity
    {
        public Account() { }
        public Account(
            int customerId,
            decimal balance,
            string currency)
        {
            CustomerId = customerId;
            Balance = balance;
            Currency = currency.ToUpper();
            AccountNumber = GenerateAccauntNumber();
        }

        public int CustomerId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Currency { get; set; } = string.Empty;

        private string GenerateAccauntNumber()
        {
            var accNumber = new StringBuilder("UZ");
            var rand = new Random();
            for (var i = 0; i < 10; i++)
            {
                var number = rand.Next(9);
                accNumber.Append(number);
            }

            return string.Empty;
        }
    }
}
