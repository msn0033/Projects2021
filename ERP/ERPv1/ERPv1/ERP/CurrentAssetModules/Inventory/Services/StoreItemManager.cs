using AutoMapper;
using ERPv1.Data;
using ERPv1.ERP.CurrentAssetModules.Inventory.Interfaces;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ERPv1.ERP.CurrentAssetModules.Inventory.Services
{
    public class StoreItemManager : IStoreItemManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IStoreItemAccountManager _storeItemAccountManager;

        public StoreItemManager(ApplicationDbContext db, IMapper mapper, IStoreItemAccountManager storeItemAccountManager)
        {
            _db = db;
            _mapper = mapper;
            _storeItemAccountManager = storeItemAccountManager;
        }
        public async Task<IEnumerable<StoreItem>> GetAllStoreItem() => await
            _db.StoreItems.Include(x => x.Brands).Include(x => x.ProductTypes).Include(x => x.UnitMeasures).ToArrayAsync();
        public async Task<StoreItem> GetStoreItemById(int Id) =>
             await _db.StoreItems.Include(x => x.Brands)
            .Include(x => x.ProductTypes).Include(x => x.UnitMeasures)
            .FirstOrDefaultAsync(x => x.Id == Id);
        public decimal GetStoreQtyById(int Id) => _db.StoreItems.FirstOrDefault(x => x.Id == Id).Qty;


        public void CreateStoreItem(StoreItemCreateVM storeItemCreateVM)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    //AutoMapper
                    var Store = _mapper.Map<StoreItem>(storeItemCreateVM);
                    //Create Account Store &Purchase
                    var Accounts = _storeItemAccountManager.GenerateStoreItemAccounts(storeItemCreateVM);

                    Store.StoreAccNum = Accounts.StoreAccNum;//رقم حساب الذي تم انشائة في  المخزن
                    Store.PurchaseAccNum = Accounts.PurchaseAccNum;//رقم حساب الذي تم انشائة في المشتريات

                    _db.StoreItems.Add(Store);//اضافة (الصنف/المنتج) بعد انشاء له حسابين في المخزن والمشتريات

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
