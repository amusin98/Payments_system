using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    public class User
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Card> Cards { get; set; }
        public User()
        {
            Cards = new HashSet<Card>();
        }

    }
}
