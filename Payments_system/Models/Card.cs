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

        public Account Account { get; set; }
    }
}
