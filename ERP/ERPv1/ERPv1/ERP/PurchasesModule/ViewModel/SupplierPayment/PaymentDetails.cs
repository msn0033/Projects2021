using ERPv1.ERP.PurchasesModule.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment
{
    public class PaymentDetails //الدفع 
    {
        public PaymentDetails()
        {
            PaymentMethod = SupplierPaymentMethodEnum.Safe;//طريقة الدفع الافتراضية الخزنة
            IsSafe = true;// الخزنة 
            IsBank = false;//البنك
            IsCheck = false;//شيك
            IsCustody = false;
        }
       
        public SupplierPaymentMethodEnum PaymentMethod { get; set; }//طريقة الدفع
        public decimal PaymentAmount { get; set; }//المبلغ المدفوع

       
        public string PaymentDate { get; set; }//تاريخ الدفع

        public string SafeAccNum { get; set; }//اذا طريقة الدفع خزنة احتاج رقم الخزنة اخزنها في هذا المتغير
        public string BankAccNum { get; set; }// اذا طريقة الدفع بنك احتاج رقم البنك في شجرة الحسابات اخزنها في هذا المتغير
        public string CheckNum { get; set; }// اذا طريقة الدفع شيك احتاج رقم الشيك اخزنه في هذا المتغير
        public string CustodyAccNum { get; set; }// طريقة الدفع من العهدة


        public string WritingDate { get; set; }// اذا طريقة الدفع شيك احتاج كتابة تاريخ كتابة الشيك
        public string PaymentDueDate { get; set; }//اذا طريقة الدفع شيك احتاج كتابة تاريخ استحقاق الشيك
        public string Description { get; set; }//وصف البيان
        public string RecieptNumber { get; set; }//رقم الايصال الذي رفعت به
        public bool IsSafe { get; set; }
        public bool IsBank { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCustody { get; set; }
    }
}
