using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    public class Card
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double Balance { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public Card()
        {
            Accounts = new HashSet<Account>();
        }
    }
}
