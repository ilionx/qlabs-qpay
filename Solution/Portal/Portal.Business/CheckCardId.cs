using Portal.DataAccess;
using Portal.Interfaces;

namespace Portal.Business
{
    public class CheckCardId : ICheckIfCardIsAttachedToUser
    {
        private readonly GetUserWithCardId _getUserWithCardId;

        public CheckCardId(GetUserWithCardId getUserWithCardId)
        {
            _getUserWithCardId = getUserWithCardId;
        }

        public bool CheckIfCardIsAvailible(string cardId)
        {
            return _getUserWithCardId.GetCardInfo(cardId);
        }
    }
}