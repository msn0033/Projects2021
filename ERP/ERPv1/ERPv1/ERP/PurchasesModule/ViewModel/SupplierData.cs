using ERPv1.CRM.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.PurchasesModule.ViewModel
{
    public class SupplierData // بيانات المورد التي ساحتاجها في الشراء او الدفع للمورد
    {
       
        public int SupplierId { get; set; }//رقم المورد
        public string SupplierName { get; set; }//اسم المورد
        public string Phone { get; set; }//جوال المورد
        public decimal Balance { get; set; }//رصيد المورد
    }
}
