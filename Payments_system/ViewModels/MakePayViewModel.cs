using Microsoft.EntityFrameworkCore;
using Payments_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.ViewModels
{
    public class MakePayViewModel
    {
        public IQueryable<Goal> Goals { get; set; }
        public IQueryable<Card> Cards { get; set; }
        public IQueryable<Account> Accounts{ get; set; }
    }
}
