using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using API.Entities;

namespace API.Controllers
{
   
    [Route("api/charges")]
    public class ChargesController : Controller
    {
        [HttpPost]
        public Stripe.Charge CreateCharge([FromBody] Entities.Customer createOptions)
        {
            var options = new ChargeCreateOptions
            {
                Amount = createOptions.Amount,
                Currency = "aud",
                Source = "tok_visa",
                ReceiptEmail = "hello_dotnet@example.com",
            };
            var service = new ChargeService();
            var charge = service.Create(options);
            return charge;
        }
    }
}