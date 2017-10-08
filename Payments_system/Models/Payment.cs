using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int GoalId { get; set; }
        public Goal Goal { get; set; }
        public string Date { get; set; }
        public double Amount { get; set; }
    }
}
