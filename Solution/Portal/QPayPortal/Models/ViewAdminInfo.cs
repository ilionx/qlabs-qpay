using Portal.Business.Models;
using System.Collections.Generic;

namespace QPayPortal.Models
{
    public class ViewAdminInfo
    {
        public IEnumerable<Employee> Employee { get; set; }
        public IEnumerable<Transaction> Transaction { get; set; }
        public IEnumerable<Transaction> OpenTransactions { get; set; }
        public IEnumerable<TerminalAndProduct> TerminalAndProducts { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}