using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.JournalModule.Services
{
    public class JournalManager : IJournalManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JournalManager(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public JournalVM NewJournal()
        {
            return new JournalVM();
        }
        public string SaveJournal(JournalVM vm)
        {
            string TransId=string.Empty;
           
            //Create journal
            var jr = new Journal();
            var date = DateTimeOffset.Now.ToString().ConvertDate();

            
            //MaxValue in db.Journal.Count and Increase the counter 1
            var MaxValue = _db.Journal.Count() > 0 ? _db.Journal.Max(x => x.TransCount) + 1 : 1;

       //    INT g= _db.Journal.Where(x => x.EntryDate.Year == date.Year && x.EntryDate.Month == date.Month).Max(x => x.TransCount);//عند بداية كل شهر يتم تصفير العداد الى 1
       //    var MaxValue = _db.Journal.Count() > 0 ? 5 + 1 : 1;


            //format date mm-yyyy-count
            jr.JournalId = date.Month.ToString() + "-" + date.Year.ToString() + "-" + MaxValue.ToString();
            jr.TransDes = vm.TransDes;
            jr.TransDate = vm.TransDate.ConvertDate();
            jr.EntryDate = date;
            jr.DocName = vm.DocName;
            jr.SystemModules = vm.SystemModules;
            jr.UserName = _httpContextAccessor.HttpContext.User.Identity.Name;

            _db.Journal.Add(jr);
            _db.SaveChanges();
            //JournalDetails=>Update Account Chart Balance
            foreach (var item in vm.JournalDetailsVM)
            {
                var account = _db.AccountChart.Find(item.AccNum);
                var parentAccount = _db.AccountChart.Find(account.ParentAcNum);
                var amount = item.Side == JournalSideEnum.Debit ? item.Debit : item.Credit;
                var LocalAmount = amount * item.UsedRate;
               
                var jrD = new JournalDetails();
                jrD.JournalId = jr.JournalId;
                jrD.AccNum = item.AccNum;
                jrD.Amount = amount;
                jrD.AmountLocal = LocalAmount;
                jrD.CurrencyId = item.CurrencyId;
                jrD.UsedRate = item.UsedRate;
                jrD.Side = item.Side;
                if (account.AccountNature == AccountNatureEnum.debit)
                {
                    if (item.Side == JournalSideEnum.Debit)
                    {
                        account.Balance += LocalAmount;
                        parentAccount.Balance += LocalAmount;
                        jrD.BalanceAfter = account.Balance;
                    }
                    if (item.Side == JournalSideEnum.Credit)
                    {
                        account.Balance -= LocalAmount;
                        parentAccount.Balance -= LocalAmount;
                        jrD.BalanceAfter = account.Balance;
                    }
                }
                if (account.AccountNature == AccountNatureEnum.Credit)
                {
                    if (item.Side == JournalSideEnum.Debit)
                    {
                        account.Balance -= LocalAmount;
                        parentAccount.Balance -= LocalAmount;
                        jrD.BalanceAfter = account.Balance;
                    }
                    if (item.Side == JournalSideEnum.Credit)
                    {
                        account.Balance += LocalAmount;
                        parentAccount.Balance += LocalAmount;
                        jrD.BalanceAfter = account.Balance;
                    }

                }
                _db.AccountChart.Update(account);
                _db.AccountChart.Update(parentAccount);
                _db.JournalDetails.Add(jrD);
                _db.SaveChanges();
                 TransId= jr.JournalId;       
            }//end loop
            return TransId;
        }


        //   currency ,Rate   الهدف منه الوصول الى    
        public AccountChart GetAccountDetails(string Id)=>
              _db.AccountChart.Include(x => x.Currency)
                   .FirstOrDefault(x => x.AccNum == Id);
        
    }
}
