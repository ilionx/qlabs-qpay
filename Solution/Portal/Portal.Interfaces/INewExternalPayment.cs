using System.Threading.Tasks;

namespace Portal.Interfaces
{
    public interface INewExternalPayment
    {
        Task<string> CreateNewPayment(string amount);
        Task<string> ExecutePayment(string paymentId, string token, string payerId, string employeeEmail);
    }
}