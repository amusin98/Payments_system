using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string GoalName { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public Goal()
        {
            Payments = new HashSet<Payment>();
        }
    }
}
