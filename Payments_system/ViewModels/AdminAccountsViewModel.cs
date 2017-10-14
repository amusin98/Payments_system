using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    //View model for display accounts on admin page
    public class AdminAccountsViewModel
    {
        public IEnumerable<Account> Accounts { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterAccountsViewModel FilterViewModel { get; set; }
        public SortAccountsViewModel SortViewModel { get; set; }
    }
}
