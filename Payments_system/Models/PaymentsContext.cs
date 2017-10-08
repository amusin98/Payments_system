using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    public class PaymentsContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public PaymentsContext(DbContextOptions<PaymentsContext> options): base(options)
        {
        }
    }
}
