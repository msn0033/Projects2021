using ERPv1.ERP.SalesModule.ViewModel;
using ERPv1.Infrastructure;

namespace ERPv1.ERP.SalesModule.Interfaces
{
    public interface ISalesManager
    {
        SalesContainer NewSales(int Id);
        FeedBack SaveNewSale(SalesContainer vm);
    }
}