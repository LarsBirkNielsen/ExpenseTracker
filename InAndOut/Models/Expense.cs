using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Name of the expense")]
        [Required]
        public string ExpenseName { get; set; }

        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="Amount must be greater than 0!")] //For at vise ErrorMessage skal det CreateNewExpense i Index view
        public int Amount { get; set; }

        [DisplayName("Expense Type")]
        public int ExpenseTypeId { get; set; }

        [ForeignKey("ExpenseTypeId")] //For at lave forbindelsen mellem de to modeller specificere vi at ExpenseTypeId er foreignKey i vores ExpenseType propperty 
        public virtual ExpenseType ExpenseType { get; set; } //Vi laver en virtuel propperty af vores ExpenseType model
    }
}
