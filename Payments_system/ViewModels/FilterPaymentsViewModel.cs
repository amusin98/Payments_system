using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.ViewModels
{
    //View model for filter payments
    public class FilterPaymentsViewModel
    {
        public FilterPaymentsViewModel(string goal, string date, int? account, int? payment)
        {
            SelectedAccount = account;
            SelectedDate = date;
            SelectedGoal = goal;
            SelectedPayment = payment;
        }
        public string SelectedGoal { get; set; }
        public int? SelectedAccount { get; set; }
        public int? SelectedPayment { get; set; }
        public string SelectedDate { get; set; }
    }
}
