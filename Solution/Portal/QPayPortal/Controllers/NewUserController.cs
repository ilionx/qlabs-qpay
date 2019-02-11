using Microsoft.AspNetCore.Mvc;
using Portal.Business;
using Portal.Business.Models;
using Portal.DataAccess;
using System.Threading.Tasks;

namespace QPayPortal.Controllers
{
    public class NewUserController : Controller
    {
        private readonly RequestUserData _requestUserData;
        private readonly RegisterNewUser _registerNewUser;
        private readonly RequestNewCards _requestNewCards;

        private readonly PortalContext _context;

        public NewUserController(RequestUserData requestUserData, RegisterNewUser registerNewUser, RequestNewCards requestNewCards, PortalContext portalContext)
        {
            _requestUserData = requestUserData;
            _registerNewUser = registerNewUser;
            _requestNewCards = requestNewCards;

            _context = portalContext;
        }

        public IActionResult Index()
        {
            var returnFromGetUserInfo = _requestUserData.RequestUser(User.Identity.Name);
            if (LoggedInUserData.AccountLevel > 0)
            {
                return RedirectToAction("Index", "Employees");
            }
            return View();
        }

        //Return SelectCard page with the cardId's and time of scan
        public async Task<IActionResult> SelectCard()
        {
            return View(_requestNewCards.RequstAllNewCards());
        }

        public IActionResult CardSelected(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            bool isCardAvailible = _registerNewUser.RegisterNewCardToUser(id, User.Identity.Name);
            if (isCardAvailible == false)
            {
                return NotFound(); //The card is already in use, please contact the admin to resolve this issue.
            }
            return RedirectToAction("Index", "Employees");
        }
    }
}