using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel.Expense;
using System.Collections.Generic;

namespace ERPv1.ERP.PurchasesModule.Interfaces.Expense
{
    public interface IExpensesManager
    {
        void AddNewExpenseItem(ExpenseCreationVM vm);
        List<ExpenseItem> GetAllExpenseItem();
        void SaveNewExpense(ExpenseContainerVM vm);
    }
}