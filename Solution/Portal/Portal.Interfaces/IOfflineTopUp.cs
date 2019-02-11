using System.Threading.Tasks;

namespace Portal.Interfaces
{
    public interface IOfflineTopUp
    {
        Task<bool> SaveOfflineTopUpTransaction(string employeeEmail, decimal topUpAmount);
    }
}