using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Portal.DataAccess.Models;
using Portal.Interfaces;

namespace Portal.DataAccess
{
    public class SaveCardToUser : ISaveCardToUser
    {
        private readonly ILogger _logger;
        private readonly PortalContext _context;

        public SaveCardToUser(PortalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<SaveCardToUser>();
        }

        public bool CreateNewUserWithCard(string cardId, string employeeEmail)
        {
            var newEmployee = new Employee
            {
                Email = employeeEmail,
                Balance = 0,
                CardUid = cardId
            };

            try
            {
                _context.Add(newEmployee);
                _context.SaveChanges();
                RemoveCardFromNewCards(cardId);
                return true;
            }
            catch (SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
                return false;
            }
        }

        public void RemoveCardFromNewCards(string cardId)
        {
            var card = new NewCards { NewCardUid = cardId };
            //_context.NewCards.Attach(card);
            _context.NewCards.Remove(card);
            _context.SaveChanges();
        }
    }
}