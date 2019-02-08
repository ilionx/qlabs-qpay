using Microsoft.Extensions.Logging;
using PaymentTerminal.DataAccess.Models;
using PaymentTerminal.Interfaces;
using System;
using System.Linq;

namespace PaymentTerminal.DataAccess
{
    public class WriteNewCard : IWriteNewCard
    {
        private readonly ILogger _logger;
        private readonly PaymentTerminalContext _context;

        public WriteNewCard(PaymentTerminalContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<WriteNewCard>();
        }

        public bool ScannedBefore(string cardUid)
        {
            var query = from newcard in _context.NewCards
                        where newcard.NewCardUid == cardUid
                        select newcard;
            try
            {
                if (query.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException error)
            {
                _logger.LogError("While running this error showed up:", error);
                return false;
            }
        }

        public void SaveNewCard(string cardUid, string scannedAt)
        {
            bool scannedBefore = ScannedBefore(cardUid);
            var newCard = new NewCards
            {
                ScanTime = DateTime.UtcNow,
                NewCardUid = cardUid,
                ScannedAt = scannedAt
            };
            try
            {
                if (scannedBefore == true)
                {
                    _context.Update(newCard);
                }
                else
                {
                    _context.Add(newCard);
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogInformation("can't connect to sql DB:" + e);
            }
        }
    }
}