using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
        public bool IsBlocked { get; set; }
        public double Balance { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public Account()
        {
            Payments = new HashSet<Payment>();
        }
    }
}
