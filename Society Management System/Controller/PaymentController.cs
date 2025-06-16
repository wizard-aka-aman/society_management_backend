using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        [HttpPost("CreateCheckout")]
       public IActionResult CreateCheckout(PaymentDto paymentdto)
        {
            var currency = "INR";
            var successUrl = "http://localhost:4200/success?billId="+paymentdto.billId;
            var cancleUrl = "http://localhost:4200/cancel";

            StripeConfiguration.ApiKey = "sk_test_tR3PYbcVNZZ796tH88S4VQ2u";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
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
            return Ok(new { url =url});
        }
    }
}
