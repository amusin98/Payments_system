using Payments_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.ViewModels
{
    //View model for display payments on user page
    public class UserPaymentsViewModel
    {
        public IEnumerable<Payment> Payments { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterPaymentsViewModel FilterViewModel { get; set; }
        public SortPaymentsViewModel SortViewModel { get; set; }
    }
}
