using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces
{
    public interface IJournalManager
    {
        JournalVM NewJournal();
        string SaveJournal(JournalVM vm);
        public AccountChart GetAccountDetails(string Id);
    }
}