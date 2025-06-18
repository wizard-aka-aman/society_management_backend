using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Model.BillsRepo;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IBillsRepository _billsRepository;
       public PaymentController(IBillsRepository  billsRepository)
        {
              _billsRepository = billsRepository;
        }

        [HttpPost("CreateCheckout")]
        public IActionResult CreateCheckout(PaymentDto paymentdto)
        {
            var currency = "INR";
            var successUrl = "http://localhost:4200/success?session_id={CHECKOUT_SESSION_ID}";
            var cancleUrl = "http://localhost:4200/cancel";

            StripeConfiguration.ApiKey = "sk_test_tR3PYbcVNZZ796tH88S4VQ2u";
            var options = new SessionCreateOptions
            {
                Metadata = new Dictionary<string, string>
                  {
                     { "billId", paymentdto.billId.ToString() }
                  },
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount = Convert.ToInt32(paymentdto.amount)*100,
                        ProductData  = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = paymentdto.title,
                            Description = paymentdto.description
                        }
                    },
                    Quantity =1
                }
            },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancleUrl,
                // ✅ Ask for customer details
                CustomerCreation = "always",
                BillingAddressCollection = "required",

                // Optional: collect phone number
                //PhoneNumberCollection = new SessionPhoneNumberCollectionOptions
                //{
                //    Enabled = true
                //}

            };
            var url = new SessionService().Create(options).Url;
            return Ok(new { url = url, details = options });
        }

        [HttpGet("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromQuery] string sessionId)
        {
            var service = new SessionService();
            var session = await service.GetAsync(sessionId);

            if (session.PaymentStatus == "paid")
            {
                var billId = session.Metadata["billId"];

                // Call internal API or service
                await _billsRepository.PayBill(int.Parse(billId));

                return Ok(new { message = "Payment successful and bill processed." });
            }

            return BadRequest(new { message = "Payment not verified or incomplete." });
        }

    }
}
