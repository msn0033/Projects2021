using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CRM.Model;
using ERPv1.CRM.Model;
using ERPv1.ERP.CurrentAssetModules.Checks.Model;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Settings;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.JournalModule.Model;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.SalesModule.Model;
using ERPv1.HR.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ERPv1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        #region HR
                public DbSet<Department> Departments { get; set; }

        #endregion

        #region GL
        public DbSet<AccountChart> AccountChart { get; set; }
        public DbSet<AccountChartCounter> AccountChartCounter { get; set; }
        public DbSet<Journal> Journal { get; set; }
        public DbSet<JournalDetails> JournalDetails { get; set; }
        public DbSet<FinancialPeriod> FinancialPeriod { get; set; }
        public DbSet<HistoricalBalance> HistoricalBalance { get; set; }
        #endregion

        #region Settings
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<VAT> VATs { get; set; }
        #endregion

        #region CRM
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<ContactBalanceInCurrency> ContactBalanceInCurrency { get; set; }
        #endregion
        #region CurrentLiabilities
        public DbSet<NotesPayable> NotesPayables { get; set; }
        public DbSet<NotesPayableTransactionHistory> NotesPayableTransactionHistory { get; set; }
        #endregion

        #region CurrentAsset
        #region Inventory
        public DbSet<Brand> Brands { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }//الصنف - المنتج
        public DbSet<Repository> Repository { get; set; }//المخزن
        public DbSet<RepositoryStoreItem> RepositoryStoreItem { get; set; }// المخزن والصنف وكمية النصف في المخزن
        public DbSet<StoreItemBalanceDetails> StoreItemBalanceDetails { get; set; }//تفاصيل المنتج
        public DbSet<StoreTransaction> StoreTransactions { get; set; }
        public DbSet<StoreItemWithSN> StoreItemWithSN { get; set; }

        #endregion
        #region Checks
        public DbSet<Check> Check { get; set; }
        public DbSet<CheckHafza> CheckHafza { get; set; }
        public DbSet<CheckHistory> CheckHistory { get; set; }
        public DbSet<CheckLocation> CheckLocation { get; set; }
        public DbSet<CheckStatus> CheckStatus { get; set; }
        
        #endregion
        #endregion
        #region Purchase
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<SupplierTransaction> SupplierTransactions { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseItem> ExpenseItems { get; set; }
        public DbSet<ExpenseSummary> ExpenseSummaries { get; set; }
        #endregion
        #region Sales
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<ClientTransaction> ClientTransactions { get; set; }


        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<AccountChart>().HasIndex(x => x.AccountName).IsUnique();//لايتكرر 
            //builder.Entity<AccountChart>().HasIndex(x => x.AccountNameAr).IsUnique();//لايتكرر 
            builder.Entity<StoreItemBalanceDetails>().HasOne(x => x.StoreTransaction).WithMany().OnDelete(DeleteBehavior.NoAction);//relation one to Many
            builder.Entity<StoreItemWithSN>().HasOne(x => x.StoreTransaction).WithMany().OnDelete(DeleteBehavior.NoAction);//relation one to Many
            builder.Entity<ContactBalanceInCurrency>().HasKey(x => new { x.ContactId, x.CurrencyId, x.AccNum });//Composite ForeignKey
            builder.Entity<RepositoryStoreItem>().HasKey(x => new { x.RepositoryId, x.StoreItemId});//Composite ForeignKey

            //builder.Entity<Invoices>().HasKey(x => new { x.InvoiceNum,x.FinancialPeriodId});//Composite ForeignKey


            #region Seeding Department
            builder.Entity<Department>().HasData(new Department() {Id=1,DepartmentName="HR" }
                                                ,new Department() { Id=2,DepartmentName="IT"});
            #endregion
            #region Seeding Repository
            builder.Entity<Repository>().HasData(new Repository() { RepositoryId = 1, RepositoryName = "MainRepository" });

            #endregion
            #region Seeding ExpenseType
            builder.Entity<ExpenseType>().HasData(new ExpenseType() { Id = 1,ExpenseTypeName="مصروفات انتاج / تشغيل" }
                                                , new ExpenseType() { Id = 2, ExpenseTypeName = "مصروفات بيع/ تسويق" }
                                                , new ExpenseType() { Id = 3, ExpenseTypeName = "مصروفات عمومية وادارية" }
                                                , new ExpenseType() { Id = 4, ExpenseTypeName = "مصروفات تمويل" });
            #endregion
            #region seed FinancialPeriod
            builder.Entity<FinancialPeriod>().HasData(
                new FinancialPeriod { Id = 1, YearName = "2020-2021", IsActive = true });
            #endregion
            #region Seed ERP Settings

            #region Currency
            builder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    CurrencyName = "Saudi Arabia Rial",
                    CurrencyNameAr = "ريال سعودي",
                    CurrencyAbbrev = "SAR",
                    IsDefault = true,
                    Rate = 1
                },
                new Currency
                {
                    Id = 2,
                    CurrencyName = "American Dollar",
                    CurrencyNameAr = "دولار امريكي",
                    CurrencyAbbrev = "$",
                    IsDefault = false,
                    Rate = (decimal)3.75
                }
            ); ;
            #endregion
            #region Branch
            builder.Entity<Branch>().HasData(new Branch { Id = 1, BranchName = "Main" });
            #endregion
            #region Vat
            builder.Entity<VAT>().HasData(new VAT { Id = 1, VatRate = 0.15M });
            #endregion

            #endregion
            #region Seeding AccountChartCounter
            builder.Entity<AccountChartCounter>().HasData(
                new AccountChartCounter
                {
                    Id = 1,
                    AccountType = "Building",
                    AccountTypeAr = "مباني",
                    AccountCategory = AccountCategoryEnum.FixedAsset,
                    ParentAcNum = "1110000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false

                },
                new AccountChartCounter
                {
                    Id = 2,
                    AccountType = "Machines And Equipments",//الات ومعدات
                    AccountTypeAr = "اجهزة ومعدات",
                    AccountCategory = AccountCategoryEnum.FixedAsset,
                    ParentAcNum = "1120000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false

                },
                new AccountChartCounter
                {
                    Id = 3,
                    AccountType = "Furniture",//اثاث
                    AccountTypeAr = "اثاث",
                    AccountCategory = AccountCategoryEnum.FixedAsset,
                    ParentAcNum = "1130000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 4,
                    AccountType = "Safe",//الخزنة
                    AccountTypeAr = "خزنة",
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    ParentAcNum = "1210000000",
                    Count = 1,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 5,
                    AccountType = "Bank",//البنك
                    AccountTypeAr = "البنك",
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    ParentAcNum = "1220000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false,
                },
                new AccountChartCounter
                {
                    Id = 6,
                    AccountType = "Client",//العميل
                    AccountTypeAr = "عملاء",
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    ParentAcNum = "1230000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false,
                },
                new AccountChartCounter
                {
                    Id = 7,
                    AccountType = "Check",//شيك
                    AccountTypeAr = "شيكات",
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    ParentAcNum = "1240000000",
                    Count = 3,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 8,
                    AccountType = "Store",//المخزن
                    AccountTypeAr = "المخزن",
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    ParentAcNum = "1250000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 9,
                    AccountType = "Custody",//عهدة  HR
                    AccountTypeAr = "العهد",
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    ParentAcNum = "1261000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 10,
                    AccountType = "StaffAdvances",//السلف HR
                    AccountTypeAr = "السلف",
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    ParentAcNum = "1262000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 11,
                    AccountType = "SupplierAdvances",//دفعات مقدمة للموردين
                    AccountTypeAr = "دفعات مقدمة للموردين",
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    ParentAcNum = "1263000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 12,
                    AccountType = "OtherCurrentAsset",//مدينون اخرون
                    AccountCategory = AccountCategoryEnum.CurrentAsset,
                    AccountTypeAr = "مدينون اخرون",
                    ParentAcNum = "1269000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
             
                new AccountChartCounter
                {
                    Id = 13,
                    AccountType = "Suppliers",//الموردين 
                    AccountTypeAr = "الموردين",
                    AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                    ParentAcNum = "2170000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 14,
                    AccountType = "NotePayable",//شيكات موردين
                    AccountTypeAr = "شيكات موردين",
                    AccountCategory = AccountCategoryEnum.FixedAsset,
                    ParentAcNum = "2210000000",
                    Count = 1,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
 

                new AccountChartCounter
                {
                    Id = 15,
                    AccountType = "Taxes",//ضرايب
                    AccountTypeAr = "ضريبة",
                    AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                    ParentAcNum = "2220000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 16,
                    AccountType = "Creditors",//دائنون اخرون
                    AccountTypeAr = "دائنون اخرون",
                    AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                    ParentAcNum = "2230000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 17,
                    AccountType = "Accrued Expenses",//مصروفات مستحقة
                    AccountTypeAr = "مصروفات مستحقة",
                    AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                    ParentAcNum = "2240000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 18,
                    AccountType = "Advances Income",//ايرادات مقدمة
                    AccountTypeAr = "ايردات مستحقة",
                    AccountCategory = AccountCategoryEnum.ShortTermLiabilities,
                    ParentAcNum = "2250000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false
                },
                new AccountChartCounter
                {
                    Id = 19,
                    AccountType = "Income",//الايرادات
                    AccountTypeAr = "الايرادات",
                    AccountCategory = AccountCategoryEnum.Income,
                    ParentAcNum = "3110000000",
                    Count = 0,
                    BalanceSheet = false,
                    IncomeStatement = true,
                },
                new AccountChartCounter
                {
                    Id = 20,
                    AccountType = "Expense",//المصروفات العمومية والادارية
                    AccountTypeAr = "المصروفات",
                    AccountCategory = AccountCategoryEnum.Expnese,
                    ParentAcNum = "4111000000",
                    Count = 0,
                    BalanceSheet = false,
                    IncomeStatement = true,
                },
                new AccountChartCounter
                {
                    Id = 21,
                    AccountType = "Purchases",//المشتريات شراء البضاعة
                    AccountTypeAr = "المشتريات",
                    AccountCategory = AccountCategoryEnum.StorePurchase,
                    ParentAcNum = "4112000000",
                    Count = 0,
                    BalanceSheet = false,
                    IncomeStatement = true,
                },
                new AccountChartCounter
                {
                    Id = 22,
                    AccountType = "Owners",//حقوق الملكية
                    AccountTypeAr = "حقوق الملكية",
                    AccountCategory = AccountCategoryEnum.OwnerEquity,
                    ParentAcNum = "5110000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false,
                },
                new AccountChartCounter
                {
                    Id = 23,
                    AccountType = "OwnerWithdraw",//مسحوبات المالك
                    AccountTypeAr = "مسحوبات المالك",
                    AccountCategory = AccountCategoryEnum.OwnerEquity,
                    ParentAcNum = "5120000000",
                    Count = 0,
                    BalanceSheet = true,
                    IncomeStatement = false,
                }
                );
            #endregion
            #region Seding AccountChart
            builder.Entity<AccountChart>().HasData(

                new AccountChart
                {
                    AccountName = "Buildings",
                    AccountNameAr = "مباني",
                    AccNum = "1110000000",
                    AccTypeId = 1,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Machines And Equipments",
                    AccountNameAr = "اجهزة ومعدات",
                    AccNum = "1120000000",
                    AccTypeId = 2,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Furnitiures",
                    AccountNameAr = "اثاث",
                    AccNum = "1130000000",
                    AccTypeId = 3,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Safe",
                    AccountNameAr = "الخزنة",
                    AccNum = "1210000000",
                    AccTypeId = 4,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Banks",
                    AccountNameAr = "بنوك",
                    AccNum = "1220000000",
                    AccTypeId = 5,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Clients",
                    AccountNameAr = "عملاء",
                    AccNum = "1230000000",
                    AccTypeId = 6,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },

                 new AccountChart
                 {
                     AccountName = "Clients Account",
                     AccountNameAr = " حساب العملاء",
                     AccNum = "1230000001",
                     AccTypeId = 6,
                     IsParent = false,
                     CurrencyId = 1,
                     Balance = 0,
                     BranchId = 1,
                     ParentAcNum = "1230000000",
                     IsActive = true,
                     AccountNature = AccountNatureEnum.debit
                 },
                new AccountChart
                {
                    AccountName = "Checks",
                    AccountNameAr = "شيكات",
                    AccNum = "1240000000",
                    AccTypeId = 7,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Checks In Safe",
                    AccountNameAr = "شيكات في الخزنة",
                    AccNum = "1240000001",
                    AccTypeId = 7,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "1240000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Checks In Bank",
                    AccountNameAr = "شيكات في البنك",
                    AccNum = "1240000002",
                    AccTypeId = 7,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "1240000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Bounced Checks ",
                    AccountNameAr = "شيكات رجيع",
                    AccNum = "1240000003",
                    AccTypeId = 7,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "1240000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Store",
                    AccountNameAr = "مخزن",
                    AccNum = "1250000000",
                    AccTypeId = 8,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Custody",
                    AccountNameAr = "عهدة",
                    AccNum = "1261000000",
                    AccTypeId = 9,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "StaffAdvances",
                    AccountNameAr = "سلف",
                    AccNum = "1262000000",
                    AccTypeId = 10,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Suppliers Advances",
                    AccountNameAr = "دفعات مقدمة للموردين",
                    AccNum = "1263000000",
                    AccTypeId = 11,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "OtherCrrentAsset",//مدينون اخرون
                    AccountNameAr = "أصول متداولة أخرى",
                    AccNum = "1269000000",
                    AccTypeId = 12,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "VAT-Purchase",//مدينون اخرون
                    AccountNameAr = "ضريبة مشتريات",
                    AccNum = "1269000001",
                    AccTypeId = 12,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "1269000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Suppliers",
                    AccountNameAr = "موردين",
                    AccNum = "2170000000",
                    AccTypeId = 13,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "Suppliers Account ",
                    AccountNameAr = "حساب  للموردين ",
                    AccNum = "2170000001",
                    AccTypeId = 13,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "2170000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "NotePayable",
                    AccountNameAr = "شيكات موردين",
                    AccNum = "2210000000",
                    AccTypeId = 14,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "NotePayable",
                    AccountNameAr = "شيكات موردين",
                    AccNum = "2210000001",
                    AccTypeId = 14,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "2210000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "Taxes",
                    AccountNameAr = "ضرائب",
                    AccNum = "2220000000",
                    AccTypeId = 15,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "VAT-Sales",
                    AccountNameAr = "ضريبة قيمة مضافة-مبيعات",
                    AccNum = "2220000001",
                    AccTypeId = 15,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "2220000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "Creditors",
                    AccountNameAr = "دائنون",
                    AccNum = "2230000000",
                    AccTypeId = 16,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "Accrued Expenses",
                    AccountNameAr = "مصروفات مستحقة",
                    AccNum = "2240000000",
                    AccTypeId = 17,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "Advances Income",
                    AccountNameAr = "ايرادات مقدمة",
                    AccNum = "2250000000",
                    AccTypeId = 18,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "Advances Income",
                    AccountNameAr = "ايرادات مقدمة",
                    AccNum = "2250000001",
                    AccTypeId = 18,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "2250000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "Income",
                    AccountNameAr = "ايرادات",
                    AccNum = "3110000000",
                    AccTypeId = 19,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = " Income",
                    AccountNameAr = "ايرادات",
                    AccNum = "3110000001",
                    AccTypeId = 19,
                    IsParent = false,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "3110000000",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "Expenses",
                    AccountNameAr = "مصروفات",
                    AccNum = "4111000000",
                    AccTypeId = 20,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Purchases",
                    AccountNameAr = "مشتريات",
                    AccNum = "4112000000",
                    AccTypeId = 21,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.debit
                },
                new AccountChart
                {
                    AccountName = "Owners",
                    AccountNameAr = "حقوق الملكية",
                    AccNum = "5110000000",
                    AccTypeId = 22,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                },
                new AccountChart
                {
                    AccountName = "OwnerWithdraw",
                    AccountNameAr = "مسحوبات المالك",
                    AccNum = "5120000000",
                    AccTypeId = 23,
                    IsParent = true,
                    CurrencyId = 1,
                    Balance = 0,
                    BranchId = 1,
                    ParentAcNum = "",
                    IsActive = true,
                    AccountNature = AccountNatureEnum.Credit
                }
                );
            #endregion

            #region Seeding Checks
            builder.Entity<CheckLocation>().HasData(
               new CheckLocation() { Id = 1, CheckLocationEN = "Safe", CheckLocationAR = "الخزنة", IsDefault = true },
               new CheckLocation() { Id = 2, CheckLocationEN = "Bank", CheckLocationAR = "البنك", IsDefault = false },
               new CheckLocation() { Id = 3, CheckLocationEN = "BankCollected", CheckLocationAR = "محصل", IsDefault = false },
               new CheckLocation() { Id = 4, CheckLocationEN = "Client", CheckLocationAR = "مع العميل", IsDefault = false },
               new CheckLocation() { Id = 5, CheckLocationEN = "Collect Cash and Return to Client", CheckLocationAR = "تم تحصيل نقداً وارجاع الشيك للعميل", IsDefault = false }
                );

            builder.Entity<CheckStatus>().HasData(
                new CheckStatus() { Id = 1, CheckStatusEN = "Under Collection",CheckStatusAR = "تحت التحصيل", IsDefault = true },
                new CheckStatus() { Id = 2, CheckStatusEN = "Collection", CheckStatusAR = "محصل", IsDefault = false },
                new CheckStatus() { Id = 3, CheckStatusEN = "Bounced", CheckStatusAR = "رجيع ", IsDefault = false }
                );
            #endregion
        }

    }
}
