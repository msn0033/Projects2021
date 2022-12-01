using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model
{
    public enum AccountCategoryEnum//فئـة الحساب
    {
       FixedAsset=10,//اصول ثابتة
       CurrentAsset=20,//اصول متداولة
       LongTermLiabilites=30,//التزامات طويلة الاجل
       ShortTermLiabilities=40,//التزامات قصير الاجل
       OwnerEquity=50,//حقوق الملكية
       Income=60,//الايرادات
       Expnese=70,//المصروفات
       StorePurchase=80//شراء من الماجر-الــمـــخــزن
    }
}
