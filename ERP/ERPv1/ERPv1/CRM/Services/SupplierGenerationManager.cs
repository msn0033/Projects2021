using AutoMapper;
using ERPv1.CRM.Interfaces;
using ERPv1.CRM.Model;
using ERPv1.CRM.ViewModel;
using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ERPv1.CRM.Services
{
    public class SupplierGenerationManager : ISupplierGenerationManager
    {
        private ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IAccountGenerator _accountGenerator;

        public SupplierGenerationManager(ApplicationDbContext db, IMapper mapper, IAccountGenerator accountGenerator)
        {

            _db = db;
            _mapper = mapper;
            _accountGenerator = accountGenerator;
        }

        public void AddNewSupplier(ContactCreatingVM supplier)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var newSupplier = _mapper.Map<Contacts>(supplier);
                    newSupplier.IsSupplier = true;
                    if (supplier.CreateAccount)
                    {
                        var account = new CreateAccountVM();
                        account.AccountName = supplier.Name;
                        account.AccountNameAr = supplier.NameAr;
                        account.AccTypeId = 13;
                        account.BranchId = supplier.BranchId;
                        account.CurrencyId = supplier.CurrencyId;
                        newSupplier.SupplierAccNum = _accountGenerator.CreateNewAccount(account);
                    }
                    _db.Contacts.Add(newSupplier);
                    _db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }



        }

        public IEnumerable<Contacts> GetAllSuppliers() => _db.Contacts.Where(x => x.IsSupplier == true).ToList();
        public Contacts GetSupplierById(int Id) => _db.Contacts.FirstOrDefault(x => x.IsSupplier == true && x.Id == Id);
    }
}
