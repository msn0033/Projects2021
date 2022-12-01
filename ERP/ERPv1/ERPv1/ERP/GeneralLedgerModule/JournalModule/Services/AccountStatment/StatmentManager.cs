using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces.AccountStatment;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel.AccountStatment;
using ERPv1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.Services.AccountStatment
{
    public class StatmentManager : IStatmentManager
    {
        private readonly ApplicationDbContext _db;

        public StatmentManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public void UpdateStatment(StatmentContainer vm)//دالة كشف الحساب
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

            var transaction = _db.JournalDetails.Include(x => x.Journal)
                             .Where(x => x.AccNum == STParm.AccNum
                                    &&
                                    x.Journal.TransDate < Start).OrderBy(x => x.Journal.EntryDate).ToList();
            if (transaction.Count > 0)
            {
                return transaction.Last().BalanceAfter;
            }
            else
            { return 0; }
        }
        public List<StatmentTransaction> GetTransactions(StatmentParams STParm, DateTime Start, DateTime End)
        {
            var Statment = _db.JournalDetails.Include(x => x.Journal)
                            .Where(x => x.AccNum == STParm.AccNum
                                    &&
                                    x.Journal.TransDate >= Start
                                    &&
                                    x.Journal.TransDate < End).OrderBy(x => x.Journal.EntryDate)

                            .Select(x => new StatmentTransaction()
                            {
                                TransDate = x.Journal.TransDate.ToString("dd/MM/yyyy"),
                                JornalId=x.Journal.JournalId,
                                Description = x.Journal.TransDes,
                                Debit = x.Side == Model.JournalSideEnum.Debit ? x.Amount : 0,
                                Credit = x.Side == Model.JournalSideEnum.Credit ? x.Amount : 0,
                                BalanceAfter = x.BalanceAfter
                            }).ToList();
            return Statment;
        }//دالة تجيب كل القيود الحسابية حسب التاريخ
    }
}
