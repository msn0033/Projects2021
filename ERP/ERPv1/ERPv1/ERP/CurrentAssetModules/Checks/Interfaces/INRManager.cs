using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInBank;
using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.ChecksInSafe;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.SalesModule.ViewModel.Payment;

namespace ERPv1.ERP.CurrentAssetModules.Checks.Interfaces
{
    public interface INRManager
    {
        void AddNewCheck(ClientPaymentContainer vm, Currency Currency, string TransId);
        CheckInSafeContainerVM GetCheckInSafe();
        public void MoveToBank(CheckInSafeContainerVM vm);
        public CheckInBankContainerVM GetCheckInBank();
        public void CollectChecks(CheckInBankContainerVM vm);
    }
}