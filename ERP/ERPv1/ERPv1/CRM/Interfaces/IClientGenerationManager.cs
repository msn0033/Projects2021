using ERPv1.CRM.Model;
using ERPv1.CRM.ViewModel;
using ERPv1.Infrastructure;
using System.Collections.Generic;

namespace ERPv1.CRM.Interfaces
{
    public interface IClientGenerationManager
    {
        FeedBack AddNewClient(ContactCreatingVM Client);
        IEnumerable<Contacts> GetAllClients();
        Contacts GetClientById(int Id);
    }
}