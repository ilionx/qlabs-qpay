using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Business;
using Portal.Business.Models;
using QPayPortal.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace QPayPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly RequestUserData _requestUserData;
        private readonly RequestAllUsers _requestAllUsers;
        private readonly RequestAllTransactions _requestAllTransactions;
        private readonly RequestTerminalWithProduct _requestTerminalWithProduct;
        private readonly RequestUserWithUserId _requestUserWithUserId;
        private readonly EditEmployeeWithId _editEmployeeWithId;
        private readonly RequestAllProducts _requestAllProducts;
        private readonly RequestProductWithProductId _requestProductWithProductId;
        private readonly EditProductWithId _editProductWithId;
        private readonly RegisterNewProduct _registerNewProduct;
        private readonly RegisterNewTerminal _registerNewTerminal;
        private readonly RemoveTerminal _removeTerminal;
        private readonly RemoveProduct _removeProduct;
        private readonly RegisterProductToTerminal _registerProductToTerminal;
        private readonly RequestAllTransactionsWithType _requestAllTransactionsWithType;
        private readonly EditTransactionTypeWithId _editTransactionTypeWithId;

        public AdminController(RequestUserData requestUserData, RequestAllUsers requestAllUsers, RequestAllTransactions requestAllTransactions,
            RequestTerminalWithProduct requestTerminalWithProduct, RequestUserWithUserId requestUserWithUserId, EditEmployeeWithId editEmployeeWithId,
            RequestAllProducts requestAllProducts, RequestProductWithProductId requestProductWithProductId, EditProductWithId editProductWithId,
            RegisterNewProduct registerNewProduct, RegisterNewTerminal registerNewTerminal, RemoveTerminal removeTerminal, RemoveProduct removeProduct,
            RegisterProductToTerminal registerProductToTerminal, RequestAllTransactionsWithType requestAllTransactionsWithType, EditTransactionTypeWithId editTransactionTypeWithId)
        {
            _requestUserData = requestUserData;
            _requestAllUsers = requestAllUsers;
            _requestAllTransactions = requestAllTransactions;
            _requestTerminalWithProduct = requestTerminalWithProduct;
            _requestUserWithUserId = requestUserWithUserId;
            _editEmployeeWithId = editEmployeeWithId;
            _requestAllProducts = requestAllProducts;
            _requestProductWithProductId = requestProductWithProductId;
            _editProductWithId = editProductWithId;
            _registerNewProduct = registerNewProduct;
            _registerNewTerminal = registerNewTerminal;
            _removeTerminal = removeTerminal;
            _removeProduct = removeProduct;
            _registerProductToTerminal = registerProductToTerminal;
            _requestAllTransactionsWithType = requestAllTransactionsWithType;
            _editTransactionTypeWithId = editTransactionTypeWithId;
        }

        public IActionResult Index()
        {
            OverviewIncome overviewIncome = new OverviewIncome();
            overviewIncome.IncomeThisMonth = _requestAllTransactions.RequestIncomeOverviewMonth();
            overviewIncome.IncomeTotal = _requestAllTransactions.RequestIncomeOverviewTotal();
            overviewIncome.TotalOpenTransactions = _requestAllTransactions.RequestTotalOpenTransactions();
            ViewData["TotalIncome"] = overviewIncome;

            var returnRequestUserData = _requestUserData.RequestUser(User.Identity.Name);

            ViewUserInfo viewUserInfo = new ViewUserInfo();
            viewUserInfo.AccountLevel = returnRequestUserData.AccountLevel;
            viewUserInfo.Balance = returnRequestUserData.Balance;
            viewUserInfo.CardId = returnRequestUserData.CardId;

            ViewAdminInfo viewAdminInfo = new ViewAdminInfo();
            viewAdminInfo.Employee = _requestAllUsers.RequestEveryUser();
            viewAdminInfo.Transaction = _requestAllTransactions.RequestEveryTransaction();
            viewAdminInfo.TerminalAndProducts = _requestTerminalWithProduct.RequestAllTerminalsWithProduct();
            viewAdminInfo.Products = _requestAllProducts.RequestEveryProduct();
            viewAdminInfo.OpenTransactions = _requestAllTransactionsWithType.RequestEveryTransaction("OPEN");


#if DEBUG
            viewUserInfo.Balance = 999;
            viewUserInfo.CardId = "1234567890";
            viewUserInfo.AccountLevel = 2;
#endif

            //todo Should put this back to equals 2 when the debug/development account is implemented same for the authentication flags on the controllers
            if (viewUserInfo.AccountLevel == 2)
            {
                return View(viewAdminInfo);
            }

            return RedirectToAction("Index", "Home");
        }

        //EDITS
        public IActionResult EditUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _requestUserWithUserId.RequestEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, [Bind("Email,CardUid,Balance,Admin")] Employee employee)
        {
            if (id != employee.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _editEmployeeWithId.EditEmployee(employee);
                return View(employee);
            }
            return View(employee);
        }

        public IActionResult EditProduct(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _requestProductWithProductId.RequestProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(string id, [Bind("ProductId,ProductName,ProductDescription,ProductPrice")] Product product)
        {
            if (Int32.Parse(id) != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _editProductWithId.EditProduct(product);
                return View(product);
            }
            return View(product);
        }

        public async Task<IActionResult> ApproveTransaction(string id)
        {
            await _editTransactionTypeWithId.EditTransaction(id, "CLOSED");

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveTransaction(string id, [Bind("TransactionId")] Transaction transaction)
        {
            return RedirectToAction("Index", "Admin");
        }

        public class TestProductModel
        {
            public string ProductCode { get; set; }
            public SelectList ProductList { get; set; }
        }

        public async Task<IActionResult> EditTerminalProduct(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _removeTerminal.CheckIfTerminalIsValid(id);

            if (terminal == null)
            {
                return NotFound();
            }
            ViewData["Terminal"] = terminal.TerminalId;
            ViewData["TerminalDescription"] = terminal.TerminalDescription;
            var products = _requestAllProducts.RequestEveryProduct();

            var model = new TestProductModel();
            model.ProductList = new SelectList(products, "ProductId", "ProductName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTerminalProduct(string id, int productId, [Bind("ProductId")] Product product)
        {
            string terminalDescription = _removeTerminal.CheckIfTerminalIsValid(id).Result.TerminalDescription;
            await _registerProductToTerminal.RegisterProductToTerminalWithProductId(id, terminalDescription,productId);
            return RedirectToAction("Index", "Admin");
        }

        //CREATES
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind("ProductName,ProductDescription,ProductPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _registerNewProduct.RegisterProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult CreateTerminal()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTerminal([Bind("TerminalId, TerminalDescription")] Terminal terminal)
        {
            if (terminal.TerminalDescription != null)
            {
                if (ModelState.IsValid)
                {
                    await _registerNewTerminal.RegisterTerminal(terminal);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(terminal);
        }

        //DELETES
        public async Task<IActionResult> DeleteTerminal(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Terminal ophalen
            var terminal = await _removeTerminal.CheckIfTerminalIsValid(id);
            if (terminal == null)
            {
                return NotFound();
            }

            return View(terminal);
        }

        [HttpPost, ActionName("DeleteTerminal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTerminalConfirmed([Bind("TerminalId")]Terminal terminal)
        {
            await _removeTerminal.RemoveTerminalWithId(terminal.TerminalId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            //Terminal ophalen
            var product = await _removeProduct.checkIfProductIsValid(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed([Bind("ProductId")]Product product)
        {
            await _removeProduct.RemoveProductWithId(product.ProductId);
            return RedirectToAction(nameof(Index));
        }

        public class OverviewIncome
        {
            public decimal IncomeThisMonth { get; set; }
            public decimal TotalOpenTransactions { get; set; }
            public decimal IncomeTotal { get; set; }
        }
    }
}