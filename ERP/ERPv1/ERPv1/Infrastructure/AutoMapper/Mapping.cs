using AutoMapper;
using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Model;
using ERPv1.CRM.ViewModel;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using ERPv1.ERP.ERPSettings.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel.Expense;
using ERPv1.Infrastructure.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Infrastructure.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<CreateAccountVM, AccountChart>();

            CreateMap<AccountChart, AccountListVM>()
                .ForMember(dest => dest.AccTypeName, src => src.MapFrom(x => x.AccType.AccountType))
                .ForMember(dest => dest.AccTypeNameAr, src => src.MapFrom(x => x.AccType.AccountTypeAr))
                .ForMember(dest => dest.CurrencyAbbr, src => src.MapFrom(x => x.Currency.CurrencyAbbrev))
                .ForMember(dest => dest.BranchName, src => src.MapFrom(x => x.Branch.BranchName));

            CreateMap<AccountChart, UpdateAccountVM>();

            CreateMap<ContactCreatingVM, Contacts>()
                .ForMember(dest => dest.SupplierAccNum, src => src.MapFrom(x => x.AccNum));

            CreateMap<StoreItemCreateVM, StoreItem>();
            CreateMap<PurchaseSummary, Purchase>()
                .ForMember(dest => dest.PurchaseDate, src => src.MapFrom(x => x.PurchaseDate.ConvertDate()));
            CreateMap<NotesPayableCreationVM, NotesPayable>()
                                .ForMember(dest => dest.WritingDate, src => src.MapFrom(x => x.WritingDate.ConvertDate()))
                                .ForMember(dest => dest.DueDate, src => src.MapFrom(x => x.DueDate.ConvertDate()));

            CreateMap<NotesPayable, NPDetails>()
                  .ForMember(dest => dest.WritingDate, src => src.MapFrom(x => x.WritingDate.Value.ToString("dd/MM/yyyy")))
                  .ForMember(dest => dest.DueDate, src => src.MapFrom(x => x.DueDate.Value.ToString("dd/MM/yyyy")))
                  .ForMember(dest => dest.CurrencyAbbrev, src => src.MapFrom(x => x.Currency.CurrencyAbbrev))
                  .ForMember(dest => dest.SupplierName, src => src.MapFrom(x => x.Supplier.NameAr))
                  .ForMember(dest => dest.BankName, src => src.MapFrom(x => x.BankAccount.AccountNameAr));
            CreateMap<ExpenseDetailsVM, ExpenseSummary>()
                                  .ForMember(dest => dest.ExpenseDate, src => src.MapFrom(x => x.ExpenseDate.ConvertDate()));


        }
    }
}
