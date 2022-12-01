using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel;
using ERPv1.ERP.PurchasesModule.ViewModel.SupplierPayment;
using System.Collections.Generic;

namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Interfaces
{
    public interface INotesPayableManager
    {
        void SaveNewNP(NotesPayableCreationVM vm);
        void SaveNewNPHistory(NotesPayableCreationVM vm, string TransId);
        List<NPDetails> GetNPWithStatus(NotesPayableStatusEnum statusEnum);
        NPContainer GetAllNP();
        void MoveCheckToCashPayment(NPDetails np);
        void CollectNP(NPDetails np);
        void CollectCashNP(NPDetails np, PaymentDetails paymentDetails);
    }
}