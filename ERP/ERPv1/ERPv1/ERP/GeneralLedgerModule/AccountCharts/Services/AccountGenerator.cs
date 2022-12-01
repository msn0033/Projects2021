using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Services
{
    public class AccountGenerator : IAccountGenerator
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public AccountGenerator(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public string CreateNewAccount(CreateAccountVM createAccountVM)
        {
            //call Mapper
            var temp = _mapper.Map<AccountChart>(createAccountVM);

            //get data form AccountChartCounter
            var currentcount = _db.AccountChartCounter.Find(createAccountVM.AccTypeId);

            //Create number AccountNumber
            temp.AccNum = (decimal.Parse(currentcount.ParentAcNum) + currentcount.Count + 1).ToString();

            //ParentAccountNmuber
            temp.ParentAcNum = currentcount.ParentAcNum;

            //Account Nature
            var parentAcc = _db.AccountChart.FirstOrDefault(x => x.AccNum.Trim() == currentcount.ParentAcNum.Trim());
            temp.AccountNature = parentAcc.AccountNature;

            //Other Field
            temp.Balance = 0;
            temp.StartingBalance = 0;
            temp.IsActive = true;
            temp.IsParent = false;

            //Update AcountChartCounter for Count
            currentcount.Count += 1;

          
                    _db.AccountChart.Add(temp);
                    _db.AccountChartCounter.Update(currentcount);
                    _db.SaveChanges();

            return temp.AccNum;
        }

        public string GeneratorAccount(CreateAccountVM createAccountVM)
        {
            //call Mapper
            var temp = _mapper.Map<AccountChart>(createAccountVM);

            //get data form AccountChartCounter
            var currentcount = _db.AccountChartCounter.Find(createAccountVM.AccTypeId);

            //Create number AccountNumber
            temp.AccNum = (decimal.Parse(currentcount.ParentAcNum) + currentcount.Count + 1).ToString();

            //ParentAccountNmuber
            temp.ParentAcNum = currentcount.ParentAcNum;

            //Account Nature
            var parentAcc = _db.AccountChart.FirstOrDefault(x => x.AccNum.Trim() == currentcount.ParentAcNum.Trim());
            temp.AccountNature = parentAcc.AccountNature;
            
            //Other Field
            temp.Balance = 0;
            temp.StartingBalance = 0;
            temp.IsActive = true;
            temp.IsParent = false;

            //Update AcountChartCounter for Count
            currentcount.Count += 1;

            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.AccountChart.Add(temp);
                    _db.AccountChartCounter.Update(currentcount);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {

                    transaction.Rollback(); ;
                }
            }
            return temp.AccNum;
        }
    }
}
