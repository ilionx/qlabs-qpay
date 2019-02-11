using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Business;
using QPayPortal.Models;
using System.Diagnostics;

namespace QPayPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly RequestUserData _requestUserData;

        public HomeController(RequestUserData requestUserData)
        {
            _requestUserData = requestUserData;
        }

        public IActionResult Index()
        {
            var returnFromGetUserInfo = _requestUserData.RequestUser(User.Identity.Name);

#if DEBUG
            returnFromGetUserInfo.IsUserRegistered = true;
            returnFromGetUserInfo.Balance = 999;
            returnFromGetUserInfo.CardId = "1234567890";
            returnFromGetUserInfo.AccountLevel = 2;
#endif

            if (returnFromGetUserInfo.AccountLevel > 0)
            {
                return RedirectToAction("Index", "Employees");
            }

            return RedirectToAction("Index", "NewUser");
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}