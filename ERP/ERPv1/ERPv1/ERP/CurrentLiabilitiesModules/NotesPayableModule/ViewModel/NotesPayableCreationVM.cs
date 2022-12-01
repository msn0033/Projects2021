namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel
{
    public class NotesPayableCreationVM
    {
        public string ChkNum { get; set; }//رقم الشيك
        public string WritingDate { get; set; }//تاريخ كتابة الشيك
        public string DueDate { get; set; }//تاريخ استحقاق الشيك
        public decimal AmountLocal { get; set; }//المبلغ بالعملة المحلية
        public decimal AmountForgin { get; set; }//المبلغ بعملة اخرى مثل الدولار
        public int CurrencyId { get; set; }//العملة
        public string BankAccountNum { get; set; }//رقم الحساب البنكي الذي بينخصم منه مبلغ  الشيك- الشيك مرتبط في اي حساب بنك
        public int SupplierId { get; set; }// المورد الذي سيصرف له الشيك
    }
}