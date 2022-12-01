using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.PurchasesModule.ViewModel
{
    public class PurchaseItemDetails// تفاصيل المنتج
    {
        public int StoreItemId { get; set; }//ProductId
        public decimal QTY { get; set; }//الكمية
        public decimal UnitPrice { get; set; }//سعر الحبة
        public string ExpiryDate { get; set; }//تاريخ الانتهاء
        public string SN { get; set; }//سريال نبر
    }
}
