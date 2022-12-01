using ERPv1.CRM.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.HR.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Model
{
    [Table("Finance_Expense_ExpenseSummary")]

    public class ExpenseSummary//ملخص المصروفات بتاعتي
    {
        public int Id { get; set; }
        public int ExpenseItemId { get; set; }
        public int? SupplierId { get; set; }//مش لازم احط المورد في المصروف
        public DateTime ExpenseDate { get; set; }

        public int PaymentAccNum { get; set; }

        public int MyProperty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public bool IsVAT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountVAT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountWithVAT { get; set; }

        public string ReferanceAccNUm { get; set; }

        public int CurrencyId { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]

        public decimal LocalAmount { get; set; }
        public int? CostCenterId { get; set; }


        //Mapping Props

        [ForeignKey("CostCenterId")]
        public Department Department { get; set; }
       
      
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        
        [ForeignKey("SupplierId")]
        public Contacts Supplier { get; set; }


        [ForeignKey("ExpenseItemId")]
        public ExpenseItem ExpenseItem { get; set; }

    }
}
