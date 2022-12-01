using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces
{
    public interface IStoreItemManager
    {
        void CreateStoreItem(StoreItemCreateVM storeItemCreateVM);
        Task<IEnumerable<StoreItem>> GetAllStoreItem();
        Task<StoreItem> GetStoreItemById(int Id);
        decimal GetStoreQtyById(int Id);
    }
}