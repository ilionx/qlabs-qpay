using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Portal.Business;
using System.Threading.Tasks;
using BraintreeHttp;
using PayPal.Core;
using PayPal.v1.Payments;

namespace QPayPortal.Controllers
{
    public class PaymentController : Controller
    {
        private readonly RegisterOfflineTopUp _registerOfflineTopUp;
        private readonly RequestOpenTopUps _requestOpenTopUps;
        private readonly ExternalPayment _externalPayment;

        public PaymentController(RegisterOfflineTopUp registerOfflineTopUp, RequestOpenTopUps requestOpenTopUps,ExternalPayment externalPayment)
        {
            _registerOfflineTopUp = registerOfflineTopUp;
            _requestOpenTopUps = requestOpenTopUps;
            _externalPayment = externalPayment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Offline()
        {
            return View();
        }

        public IActionResult ExternalIndex()
        {
            return View();
        }

        public IActionResult Failed()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalIndex(string paymentProvider)
        {
            //For now the amount is hardcoded 10, but it's possible to add textbox input if needed
            return Redirect(_externalPayment.SelectPaymentProvider(paymentProvider,"15").Result);
        }

        [HttpGet]
        public async Task<IActionResult> Execute(string paymentId,string token,string payerId)
        {
            ViewData["TransactionId"] = paymentId;
            ViewData["PaymentProvider"] = "PayPal";
            return View(_externalPayment.ExecutePayPalPayment(paymentId, token, payerId, User.Identity.Name).Result);
        }

        [Route("Payment/OfflineTopUp/{amount}")]
        public async Task<IActionResult> OfflineTopUp(decimal amount)
        {
            //Check of de user nog een OPEN topUp heeft staan
            if (await _requestOpenTopUps.RequestOpenTopUpsFromEmployee(User.Identity.Name) != null)
            {
                return View("FailedStillOpenTopUp");
            }
            //Als er geen OPEN meer staat door met opwaarderen, anders show FailedStillOpenTopUp
            if (_registerOfflineTopUp.RegisterTopUp(User.Identity.Name, amount).Result)
            {
                return View("OfflineComplete");
            }
            return View("Failed");
        }
    }
}