using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ERP.GeneralLedgerModule.AccountCharts.Services
{
    public class AccountListManager : IAccountListManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public AccountListManager(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }
        public IEnumerable<AccountListVM> GetAllAccount() =>
            _mapper.Map<List<AccountListVM>>(_db.AccountChart.
                Include(x => x.AccType).
                Include(x => x.Currency).
                Include(x => x.Branch).Where(x => x.IsParent == false).ToList()).OrderBy(x=>x.AccNum);
     
        
        public UpdateAccountVM GetAccount(string AccNum) =>
                            _mapper.Map<UpdateAccountVM>(_db.AccountChart.Find(AccNum));

        
    }
}
