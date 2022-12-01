using ERPv1.ERP.SalesModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.ViewModel.Payment
{
    public class ClientPaymentDetails
    {
        public ClientPaymentDetails()
        {
            IsSafe = true;
        }

        public ClientPaymentMethodEnum  PaymentMethod{ get; set; }//طريقة الدفع
        public decimal PaymentAmount { get; set; }//المبلغ المدفوع

        public string PaymentDate { get; set; }//تاريخ الدفع
        public string SafeAccNum { get; set; }//اذا طريقة الدفع خزنة احتاج رقم الخزنة اخزنها في هذا المتغير
        public string BankAccNum { get; set; }// اذا طريقة الدفع بنك احتاج رقم البنك في شجرة الحسابات اخزنها في هذا المتغير
        public string CheckNum { get; set; }// اذا طريقة الدفع شيك احتاج رقم الشيك اخزنه في هذا المتغير
        public string OriginalBank { get; set; }//الشيك على اي بنك اتعمل
        //public string WritingDate { get; set; }// اذا طريقة الدفع شيك احتاج كتابة تاريخ كتابة الشيك
        public string PaymentDueDate { get; set; }//اذا طريقة الدفع شيك احتاج كتابة تاريخ استحقاق الشيك
        public string Description { get; set; }//وصف البيان
        public string RecieptNumber { get; set; }//رقم الايصال الذي رفعت به
        public bool IsSafe { get; set; }
        public bool IsBank { get; set; }
        public bool IsCheck { get; set; }
    }
}
