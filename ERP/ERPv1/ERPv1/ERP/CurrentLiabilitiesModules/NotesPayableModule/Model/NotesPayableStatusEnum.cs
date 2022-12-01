namespace ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model
{
    public enum NotesPayableStatusEnum//طريقة سداد الاوراق المالية
    {
        UnderCollection = 10,//تحت التحصيل-لسا معادة لم يجي لتحصيل
        CashCollection = 20,//كاش
        Collected = 30, //راح البنك وتحصل الشيك لاتوجد مشاكل
        Cancelled = 40//الغاء الشيك
    }
}