using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models.ViewModels
{
    public class ExpenseViewModel
    {
        public Expense Expense { get; set; }
        public IEnumerable<SelectListItem> ExpenseTypeDropDown { get; set; }

        //Grunden til viewmodels er at samle de forskellige models som view'et skal bruge.
        //På den måde skal et view ikke bruge to eller forskellige models.
        //F.eks. brugte Expense/Create view @model Expense og en viewbag fra Expense types. 
        //Nu bruger Expense/Create-view  !KUN!  @model ExpenseViewModel (altså denne klasse) til at udføre det den skal
    }
}
