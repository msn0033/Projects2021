using Microsoft.AspNetCore.Mvc;
using PaymentApi.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Repository
{
    public interface IPaymentDetail
    {

        Task<IEnumerable<PaymentDetails>> GetPaymentDetails();
        Task<PaymentDetails> AddPaymentDetails(PaymentDetails p);
        Task<bool> deletePaymentDetails(int id);
        Task<bool> updatedeletePaymentDetails(int id, PaymentDetails form);

    }
}
