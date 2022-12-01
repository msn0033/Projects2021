namespace ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel
{
    public class StoreItemAccountHelper // فقط string هذا الكلاس تم استخدامة لارجاع 2 
    /*
     * بعد انشاء حساب في المخزن وحساب في المشتريات للصنف/المنتج
     * يتم ارجاع رقم الحسابين الذينا انشاناهم لاضافتهم للصنف /المنتج 
     * */

    {
        public string StoreAccNum { get; set; }
        public string PurchaseAccNum { get; set; }
    }
}