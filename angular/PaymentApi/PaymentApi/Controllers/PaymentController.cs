using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Model;
using PaymentApi.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentDetail _pay;

        public PaymentController(IPaymentDetail Pay)
        {
            _pay = Pay;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetails()
        {
            try
            {
                return Ok(await _pay.GetPaymentDetails());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> AddPaymentDetails(PaymentDetails p)
        {
            return Created("GetPaymentDetails", await _pay.AddPaymentDetails(p));
        }

        // DELETE: api/PayDetails/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetails(int id)
        {
            bool temp = await _pay.deletePaymentDetails(id);
            return temp ? NoContent() : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updatePaymentDetails(int id, PaymentDetails form)
        {
            bool temp = await _pay.updatedeletePaymentDetails(id, form);
            return temp ? NoContent() : NotFound();

        }


    }
}
