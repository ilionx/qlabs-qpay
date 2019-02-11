using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Business;
using QPayPortal.Models;

namespace QPayPortal.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly RequestUserData _requestUserData;

        public EmployeesController(RequestUserData requestUserData)
        {
            _requestUserData = requestUserData;
        }

        public IActionResult Index()
        {
            var returnRequestUserData = _requestUserData.RequestUser(User.Identity.Name);

            ViewUserInfo viewUserInfo = new ViewUserInfo();
            viewUserInfo.AccountLevel = returnRequestUserData.AccountLevel;
            viewUserInfo.Balance = returnRequestUserData.Balance;
            viewUserInfo.CardId = returnRequestUserData.CardId;
            viewUserInfo.AmountOpenTransactions = returnRequestUserData.AmountTransactions;

#if DEBUG
            viewUserInfo.Balance = 999;
            viewUserInfo.CardId = "1234567890";
            viewUserInfo.AccountLevel = 2;
#endif

            ViewData["LoggedInUserData"] = viewUserInfo;

            if (viewUserInfo.AccountLevel > 0)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult TopUp()
        {
            return RedirectToAction("Offline", "Payment");
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //[AllowAnonymous]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}