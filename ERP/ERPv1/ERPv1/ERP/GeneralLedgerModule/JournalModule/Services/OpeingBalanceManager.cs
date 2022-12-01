using CRM.Model;
using ERPv1.Data;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.ViewModel;
using ERPv1.ERP.SalesModule.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ERP.GeneralLedgerModule.JournalModule.Services
{
    public class OpeingBalanceManager : IOpeingBalanceManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClientBalanceManager _clientBalanceManager;

        public OpeingBalanceManager(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor , IClientBalanceManager clientBalanceManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _clientBalanceManager = clientBalanceManager;
        }

        public OpeningTransactionVM NewOpeningTrans()
        {
            var vm = new OpeningTransactionVM();
            vm.CurrentFinancialPeriodId = _db.FinancialPeriod.FirstOrDefault(X => X.IsActive).Id;//جيب لي السنة المالية النشطة
            vm.TransactionDetails = _db.AccountChart.Include(x => x.Currency)
                .Where(x => x.IsParent == false)
                .Select(x => new OpeningTransactionDetailsVM()
                {
                    AccNum = x.AccNum,
                    AccountName = x.AccountNameAr,
                    Credit = 0,
                    Debit = 0,
                    Side = x.AccountNature == AccountNatureEnum.debit ? JournalSideEnum.Debit : JournalSideEnum.Credit,
                    CurrencyAbbr = x.Currency.CurrencyAbbrev,
                    CurrencyId = x.CurrencyId,
                    UsedRate = x.Currency.Rate
                }
                ).ToList();

            return vm;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        public void SaveOpeningTrans(OpeningTransactionVM vm)
        {
            // 3Table journal - journalDetails - HistoriCalBalance & UpdateBalance Account Chart

            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    //journal
                    var jr = new Journal();
                    var date = DateTimeOffset.Now;
                    //format date mm-yyyy-count
                    var MaxValue = _db.Journal.Count() > 0 ? _db.Journal.Max(x => x.TransCount) + 1 : 1;
                    //format date mm-yyyy-count
                    jr.JournalId = date.Month.ToString() + "-" + date.Year.ToString() + "-" + MaxValue.ToString();
                    jr.TransDes = vm.TransDes;
                    jr.TransDate = DateTime.Parse(vm.TransDate);
                    jr.EntryDate = date;
                    jr.SystemModules = vm.SystemModules;
                    jr.UserName = _httpContextAccessor.HttpContext.User.Identity.Name;

                    _db.Journal.Add(jr);

                    //journal Detalis

                    foreach (var item in vm.TransactionDetails)
                    {
                        var jrD = new JournalDetails();
                        jrD.JournalId = jr.JournalId;
                        jrD.AccNum = item.AccNum;
                        jrD.CurrencyId = item.CurrencyId;
                        jrD.UsedRate = item.UsedRate;
                        jrD.Side = item.Side;
                        var amounttemp = item.Side == JournalSideEnum.Debit ? item.Debit : item.Credit;
                        var amountLocaltemp = amounttemp * item.UsedRate;
                        jrD.Amount = amounttemp;
                        jrD.AmountLocal = amountLocaltemp;
                        jrD.BalanceAfter = amounttemp;

                        _db.JournalDetails.Add(jrD);


                        //update Account Chart Balances with Same Currency
                        var account = _db.AccountChart.Find(item.AccNum);
                        account.Balance = amounttemp;
                        account.StartingBalance = amounttemp;

                        _db.AccountChart.Update(account);


                        //update Parent Account With Amount Local(Sum All Child Account)
                        var parent = _db.AccountChart.Find(account.ParentAcNum);
                        parent.Balance += amountLocaltemp;
                        parent.StartingBalance += amountLocaltemp;

                        _db.AccountChart.Update(parent);


                        //Insert Historical Balance
                        var HisBalance = new HistoricalBalance();
                        HisBalance.AccNum = item.AccNum;
                        HisBalance.Balance = amounttemp;
                        HisBalance.FinancialPeriodId = vm.CurrentFinancialPeriodId;


                        _db.HistoricalBalance.Add(HisBalance);

                        //try
                        //{
                        //    bool v = _db.Contacts.Where(x => x.ClientAccNum == item.AccNum).Count() > 0 ? true : false;
                        //    if (v)
                        //    {
                        //        var cl = _db.Contacts.FirstOrDefault(x => x.ClientAccNum == item.AccNum);
                        //        cl.ClientBalance = item.Debit;
                        //        _clientBalanceManager.UpdateClientBalance(cl, item.Debit, true);

                        //        if (_db.ContactBalanceInCurrency.Count() > 0)
                        //        {
                        //           var c= _db.ContactBalanceInCurrency.FirstOrDefault(x => x.ContactId == cl.Id);
                        //            c.Balance = item.Debit;
                        //            _clientBalanceManager.ManageClientBlanceInCurrency(cl.Id, item.AccNum, item.CurrencyId, item.Debit, true);
                        //        }
                                
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    var m = ex.Message;
                            
                        //}
                        
                        


                    }

                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
            }
        }
    }
}
