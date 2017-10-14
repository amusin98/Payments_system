using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments_system.Models
{
    //View model for filter accounts
    public class FilterAccountsViewModel
    {
        public string SelectedEmail { get; set; }
        public int? SelectedCard { get; set; }
        public int? SelectedAccount { get; set; }

        public FilterAccountsViewModel(int? card, string email, int? account)
        {
            SelectedEmail = email;
            SelectedCard = card;
            SelectedAccount = account;
        }
    }
}
