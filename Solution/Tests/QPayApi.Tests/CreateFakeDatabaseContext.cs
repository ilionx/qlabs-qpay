using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using PaymentTerminal.Business;
using PaymentTerminal.DataAccess;
using PaymentTerminal.DataAccess.Models;
using PaymentTerminal.Interfaces;
using System;

namespace QPayApi.UnitTest
{
    public class CreateFakeDatabaseContext
    {
        private readonly ICheckBalance _checkBalance;
        private readonly ICheckTerminal _checkTerminal;
        private readonly IProcessPayment _processPayment;
        private readonly INewCardScanned _newCardScanned;
        private readonly ILoggerFactory _loggerFactory;
        private readonly PaymentTerminalContext _context;

        public CreateFakeDatabaseContext()
        {
            string databaseName = Guid.NewGuid().ToString();
            _context = TestContextCreater.Create(databaseName);

            _context.Employees.Add(new Employee()
            {
                CardUid = "04346C824D5380",
                Balance = 10,
                Email = "mike.vromen@qnh.nl"
            });

            _context.Employees.Add(new Employee()
            {
                CardUid = "99999999999999",
                Balance = 0,
                Email = "test.test@qnh.nl"
            });

            var terminals = new Faker<Terminal>()
                .RuleFor(x => x.TerminalId, x => Guid.NewGuid().ToString())
                .Generate(10);

            _context.Terminals.Add(new Terminal()
            {
                TerminalId = "04346C824D538012",
                ProductId = 1
            });

            _context.Terminals.AddRange(terminals);

            var products = new Faker<Product>()
                .RuleFor(x => x.ProductId, x => x.IndexFaker + 1)
                .RuleFor(x => x.Productname, x => x.Commerce.ProductName())
                .RuleFor(x => x.ProductDescription, x => x.Lorem.Sentence())
                .RuleFor(x => x.ProductPrice, x => Convert.ToDecimal((x.Commerce.Price(0.5m, 2m))))
                .RuleFor(x => x.Terminal, x => x.PickRandom(terminals))
                .Generate(10);
            _context.Products.AddRange(products);

            var loggerFactory = new LoggerFactory();

            var getBalance = new GetBalance(_context);
            _checkBalance = new CheckBalance(getBalance, loggerFactory);

            var getProduct = new GetProduct(_context);
            _checkTerminal = new CheckTerminal(getProduct, loggerFactory);

            var writeTransaction = new WriteTransaction(_context, loggerFactory);
            _processPayment = new ProcessPayment(writeTransaction);

            var writeNewcard = new WriteNewCard(_context, loggerFactory);
            _newCardScanned = new NewCardScanned(writeNewcard, getProduct);

            _context.SaveChanges();
        }

        public ValidateScan GiveContext()
        {
            return (new ValidateScan(_checkBalance, _checkTerminal, _processPayment, _newCardScanned));
        }

        public static class TestContextCreater
        {
            public static PaymentTerminalContext Create(string uniqueInMemoryDatabaseName)
            {
                var options = new DbContextOptionsBuilder<PaymentTerminalContext>()
                    .UseInMemoryDatabase(databaseName: uniqueInMemoryDatabaseName)
                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .Options;
                return new PaymentTerminalContext(options);
            }
        }
    }
}