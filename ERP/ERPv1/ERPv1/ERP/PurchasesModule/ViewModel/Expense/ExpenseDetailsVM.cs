using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.Expense
{
    public class ExpenseDetailsVM
    {
        public int ExpenseItemId { get; set; }
        public int? SupplierId { get; set; }// غير ملزم اختيار المورد عند تسجيل المصروف
        public string ExpenseDate { get; set; }
        public decimal Amount { get; set; }

        public decimal VATAmount { get; set; }
        public decimal TotalWithVAT { get; set; }
        public int CurrencyId { get; set; }
        public int? CostCenterId { get; set; }

    }

}
