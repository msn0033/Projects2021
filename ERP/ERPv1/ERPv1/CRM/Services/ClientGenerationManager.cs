using AutoMapper;
using ERPv1.CRM.Interfaces;
using ERPv1.CRM.Model;
using ERPv1.CRM.ViewModel;
using ERPv1.Data;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using ERPv1.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.CRM.Services
{
    public class ClientGenerationManager : IClientGenerationManager
    {
        private ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IAccountGenerator _accountGenerator;
        public ClientGenerationManager(ApplicationDbContext db, IMapper mapper, IAccountGenerator accountGenerator)
        {
            _db = db;
            _mapper = mapper;
            _accountGenerator = accountGenerator;
        }
        public FeedBack AddNewClient(ContactCreatingVM Client)
        {
            var feedback = new FeedBack();
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var newClient = _mapper.Map<Contacts>(Client);

                    newClient.IsClient = true;
                    //انشاء حساب رئيسي في شجرة الحسابات
                    if (Client.CreateAccount)
                    {
                        var account = new CreateAccountVM();
                        account.AccountName = Client.Name;
                        account.AccountNameAr = Client.NameAr;
                        account.AccTypeId = 6;
                        account.BranchId = Client.BranchId;
                        account.CurrencyId = Client.CurrencyId;
                        
                        newClient.ClientAccNum = _accountGenerator.CreateNewAccount(account);
                    }
                    newClient.ClientAccNum = Client.AccNum;
                    newClient.SupplierAccNum = null;
                    _db.Contacts.Add(newClient);
                    _db.SaveChanges();
                    transaction.Commit();
                    feedback.Done = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    feedback.Done = false;
                    do
                    {
                        feedback.Errors.Add(ex.Message);
                        ex = ex.InnerException;
                    } while (ex!=null );
                }
            }
            return feedback;
        }

        public IEnumerable<Contacts> GetAllClients() => _db.Contacts.Where(x => x.IsClient == true).ToList();
        public Contacts GetClientById(int Id) => _db.Contacts.FirstOrDefault(x => x.IsClient == true && x.Id == Id);
    }
}
