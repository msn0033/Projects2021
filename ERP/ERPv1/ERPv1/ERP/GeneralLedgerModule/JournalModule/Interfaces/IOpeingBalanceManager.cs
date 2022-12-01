using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces
{
    public interface IOpeingBalanceManager
    {
        OpeningTransactionVM NewOpeningTrans();
        public void SaveOpeningTrans(OpeningTransactionVM vm);
    }
}