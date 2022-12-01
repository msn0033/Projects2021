using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.SalesModule.Interfaces.ClientStatment;
using ERPv1.ERP.SalesModule.ViewModel.ClientStatment;
using ERPv1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.SalesModule.Services.ClientStatment
{
    public class ClientReport : IClientReport
    {
        private readonly ApplicationDbContext _db;

        public ClientReport(ApplicationDbContext db)
        {
            _db = db;
        }

        public void UpdateStatment(ClientStatmentContainer vm)//دالة كشف الحساب
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

            var transaction = _db.ClientTransactions.Include(x => x.Journal)
                             .Where(x => x.ClientId == STParm.ClientId
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
            var Statment = _db.ClientTransactions.Include(x => x.Journal)
                            .Where(x => x.ClientId == STParm.ClientId
                                    &&
                                    x.PaymentDate >= Start
                                    &&
                                    x.PaymentDate < End).OrderBy(x => x.Id)

                            .Select(x => new StatmentTransaction()
                            {
                                TransDate = x.PaymentDate.ToString("dd/MM/yyyy"),
                                Description = x.Journal.TransDes,
                                JournalId=x.JournalId,
                                Debit = x.PaymentMethodEnum == Model.ClientPaymentMethodEnum.Credit ? x.PaymentAmount : 0,
                                Credit = x.PaymentMethodEnum != Model.ClientPaymentMethodEnum.Credit ? x.PaymentAmount : 0,
                                BalanceAfter = x.BalanceAfter,
                                InvoiceAcc = x.InvoiceNum
                            }).ToList();
            return Statment;
        }//دالة تجيب كل القيود الحسابية حسب التاريخ
    }
}
