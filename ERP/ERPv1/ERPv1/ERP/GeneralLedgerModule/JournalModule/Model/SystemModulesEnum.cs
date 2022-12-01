using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.Model
{
    public enum SystemModulesEnum // وحدات النظام
    {
        Revenue = 10,//إيرادات-مبيعات
        NR = 20,//-شيكات
        Expendeture = 30,//المصروفات-مشتريات
        GL = 40//قيود دايرك بتتعمل من السستم
    }
}
