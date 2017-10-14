using Payments_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.ViewModels
{
    //View model for sorting payments
    public class SortPaymentsViewModel
    {
        public PaymentsSortState AccIdSort { get; private set; }
        public PaymentsSortState PaymentIdSort { get; private set; }
        public PaymentsSortState GoalSort { get; private set; }
        public PaymentsSortState AmountSort { get; private set; }
        public PaymentsSortState Current { get; private set; }

        public SortPaymentsViewModel(PaymentsSortState sortOrder)
        {
            AccIdSort = sortOrder == PaymentsSortState.AccIdAsc ? PaymentsSortState.AccIdDesc : PaymentsSortState.AccIdAsc;
            PaymentIdSort = sortOrder == PaymentsSortState.PaymentIdAsc ? PaymentsSortState.PaymentIdDesc : PaymentsSortState.PaymentIdAsc;
            GoalSort = sortOrder == PaymentsSortState.GoalAsc ? PaymentsSortState.GoalDesc : PaymentsSortState.GoalAsc;
            AmountSort = sortOrder == PaymentsSortState.AmountAsc ? PaymentsSortState.AmountDesc : PaymentsSortState.AmountAsc;
            Current = sortOrder;
        }
    }
}
