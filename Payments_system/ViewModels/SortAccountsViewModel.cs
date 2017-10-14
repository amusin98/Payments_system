using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    //View model for sorting accounts
    public class SortAccountsViewModel
    {
        public AccountsSortState AccIdSort { get; private set; }
        public AccountsSortState CardIdSort { get; private set; }
        public AccountsSortState EmailSort { get; private set; }
        public AccountsSortState BalanceSort { get; private set; }
        public AccountsSortState StatusSort { get; private set; }
        public AccountsSortState Current { get; private set; }

        public SortAccountsViewModel(AccountsSortState sortOrder)
        {
            AccIdSort = sortOrder == AccountsSortState.AccIdAsc ? AccountsSortState.AccIdDesc : AccountsSortState.AccIdAsc;
            CardIdSort = sortOrder == AccountsSortState.CardIdAsc ? AccountsSortState.CardIdDesc : AccountsSortState.CardIdAsc;
            EmailSort = sortOrder == AccountsSortState.EmailAsc ? AccountsSortState.EmailDesc : AccountsSortState.EmailAsc;
            BalanceSort = sortOrder == AccountsSortState.BalanceAsc ? AccountsSortState.BalanceDesc : AccountsSortState.BalanceAsc;
            StatusSort = sortOrder == AccountsSortState.StatusAsc ? AccountsSortState.StatusDesc : AccountsSortState.StatusAsc;
            Current = sortOrder;
        }
    }
}
