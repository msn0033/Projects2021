using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel
{
    public class NPDetails
    {
        public string ChkNum { get; set; }//رقم الشيك
        public string WritingDate { get; set; }//تاريخ كتابة الشيك
        public string DueDate { get; set; }//تاريخ استحقاق الشيك
        public decimal AmountLocal { get; set; }//المبلغ بالعملة المحلية
        public decimal AmountForgin { get; set; }//المبلغ بعملة اخرى مثل الدولار
        public int CurrencyId { get; set; }//العملة
        public string CurrencyAbbrev { get; set; }
        public string BankAccountNum { get; set; }//رقم الحساب البنكي الذي بينخصم منه مبلغ  الشيك- الشيك مرتبط في اي حساب بنك
        public string BankName { get; set; }
        public int SupplierId { get; set; }// المورد الذي سيصرف له الشيك
        public string SupplierName { get; set; }
        public decimal Paid { get; set; }
    }
}
