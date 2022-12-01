using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces
{
    public interface IAccountGenerator
    {
        string GeneratorAccount(CreateAccountVM createAccountVM);
        string CreateNewAccount(CreateAccountVM createAccountVM);
    }
}