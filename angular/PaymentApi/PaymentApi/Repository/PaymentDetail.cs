using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Repository
{
    public class PaymentDetail : IPaymentDetail
    {
        private readonly DB _db;

        public PaymentDetail(DB db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<PaymentDetails>> GetPaymentDetails()
        {
            return await _db.PaymentDetails.ToListAsync();
        }


        public async Task<PaymentDetails> AddPaymentDetails(PaymentDetails p)
        {
            _db.PaymentDetails.Add(p);
            await _db.SaveChangesAsync();
            return p;
        }

        public async Task<bool> deletePaymentDetails(int id)
        {
            var x = await _db.PaymentDetails.FindAsync(id);
            if (x == null)
                return false;
            _db.PaymentDetails.Remove(x);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updatedeletePaymentDetails(int id, PaymentDetails form)
        {
            if (id != form.paymentDetalisId)
                return false;

            _db.Entry(form).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailsExists(id))
                    return false;
                else
                    throw;
            }
            return true;
        }
        private bool PaymentDetailsExists(int id)
        {
            return _db.PaymentDetails.Any(e => e.paymentDetalisId == id);
        }
    }
}
