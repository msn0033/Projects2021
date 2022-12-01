using ERPv1.ERP.CurrentAssetModules.Checks.ViewModel.CollectCashCheck;

namespace ERPv1.ERP.CurrentAssetModules.Checks.Interfaces.CollectCashCheck
{
    public interface ICollectCashCheckManager
    {
        CollectCashCheckContainerVM NewCollectCashCheck(string ChkNum);
        void SaveCollectCashCheck(CollectCashCheckContainerVM vm);
    }
}