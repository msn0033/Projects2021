using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel
{
    public class SupplierBalanceDetails//تفاصيل رصيد المورد
    {
        public int CurrencyId { get; set; }//العملة
        public decimal Amount { get; set; }//رصيد المورد بعملة مثل الدولار
        public decimal LocalAmount { get; set; }//الرصيد بالعملة المحلية
        public decimal Rate { get; set; }//سعر الصرف
        public string CurrencyAbbr { get; set; }//اختصار شكل العملة المستخدمة
    }
}
