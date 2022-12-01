using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.PurchasesModule.Interfaces.SupplierStatment;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierStatment;

using ERPv1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.PurchasesModule.Services.SupplierStatment
{
    public class SupplierReport : ISupplierReport
    {
        private readonly ApplicationDbContext _db;

        public SupplierReport(ApplicationDbContext db)
        {
            _db = db;
        }

        public void UpdateStatment(SupplierStatmentContainer vm)//دالة كشف الحساب
        {
            var Start = vm.StatmentParams.StartDate.ConvertDate();
            var End = vm.StatmentParams.EndDate.ConvertDate().AddDays(1); //ex=>01/11/2020 --->31/10/2020

            vm.StatmentTransaction = GetTransactions(vm.StatmentParams, Start, End);//جبت كل القيود المحاسبية  
            vm.StatmentParams.StartBalance = GetStartBalance(vm.StatmentParams, Start);//بداية الرصيد 
            if (vm.StatmentTransaction.Count > 0)
                vm.StatmentParams.EndBalance = vm.StatmentTransaction.Last().BalanceAfter;// نهاية الرصيد
            else
                vm.StatmentParams.EndBalance = 0;
        }

        public decimal GetStartBalance(StatmentParams STParm, DateTime Start)//يجيب لك الرصيد الافتتاحي  حسب التاريخ 
        {

            var transaction = _db.SupplierTransactions.Include(x => x.Journal)
                             .Where(x => x.SupplierId == STParm.SupplierId
                                    &&
                                    x.PaymentDate < Start).OrderBy(x => x.Id).ToList();
            if (transaction.Count > 0)
            {
                return transaction.Last().BalanceAfter;
            }
            else
            { return 0; }
        }
        public List<StatmentTransaction> GetTransactions(StatmentParams STParm, DateTime Start, DateTime End)
        {
            var Statment = _db.SupplierTransactions.Include(x => x.Journal)
                            .Where(x => x.SupplierId == STParm.SupplierId
                                    &&
                                    x.PaymentDate >= Start
                                    &&
                                    x.PaymentDate < End).OrderBy(x => x.Id)

                            .Select(x => new StatmentTransaction()
                            {
                                TransDate = x.PaymentDate.ToString("dd/MM/yyyy"),
                                JournalId = x.JournalId,
                                Description = x.Journal.TransDes,
                                Debit = x.PaymentMethodEnum != Model.SupplierPaymentMethodEnum.Credit ? x.PaymentAmount : 0,
                                Credit = x.PaymentMethodEnum == Model.SupplierPaymentMethodEnum.Credit ? x.PaymentAmount : 0,
                                BalanceAfter = x.BalanceAfter,
                               PurchaseId=x.PurchaseId
                              
                            }).ToList();
            return Statment;
        }//دالة تجيب كل القيود الحسابية حسب التاريخ

    }
}
