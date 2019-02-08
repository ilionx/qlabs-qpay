using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Portal.Business;
using System.Threading.Tasks;
using Portal.Business.Models;

namespace QPayPortal.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly RequestUserTransactions _requestUserTransactions;
        private readonly RequestUserData _requestUserData;

        public TransactionsController(RequestUserTransactions requestUserTransactions, RequestUserData requestUserData)
        {
            _requestUserTransactions = requestUserTransactions;
            _requestUserData = requestUserData;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            if (_requestUserData.RequestUser(User.Identity.Name).AccountLevel > 0)
            {
                return View(_requestUserTransactions.RequestTransactions(User.Identity.Name));
            }

#if DEBUG
            return View(new List<Transaction>
            {
                new Transaction()
                {
                    TransactionId = 1, Amount = 1, DateTime = DateTime.Now, EmployeeEmail = "debug@qpay.com",
                    TransactionType = "debug"
                }
            });
#endif

            return RedirectToAction("Index", "Home");
        }
    }
}