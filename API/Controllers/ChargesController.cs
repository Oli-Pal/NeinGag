using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using API.Entities;
using API.Interfaces;
using AutoMapper;

namespace API.Controllers
{
   
    [Route("api/charges")]
    public class ChargesController : Controller
    
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ChargesController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost]
        public Stripe.Charge CreateCharge([FromForm] Entities.Customer createOptions)
        {
            var options = new ChargeCreateOptions
            {
                Amount = createOptions.Amount,
                Currency = "pln",
                Source = "tok_visa",
                ReceiptEmail = "hello_dotnet@example.com",
                

                
            };
            var service = new ChargeService();
            var charge = service.Create(options);
            return charge;
        }
    }
}