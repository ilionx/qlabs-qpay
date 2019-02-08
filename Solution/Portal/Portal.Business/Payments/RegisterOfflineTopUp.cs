using Portal.Interfaces;
using System.Threading.Tasks;

namespace Portal.Business
{
    public class RegisterOfflineTopUp
    {
        private readonly IOfflineTopUp _offlineTopUp;

        public RegisterOfflineTopUp(IOfflineTopUp offlineTopUp)
        {
            _offlineTopUp = offlineTopUp;
        }

        public async Task<bool> RegisterTopUp(string email, decimal topUpAmount)
        {
            topUpAmount = 15;
            return await _offlineTopUp.SaveOfflineTopUpTransaction(email, topUpAmount);
        }
    }
}