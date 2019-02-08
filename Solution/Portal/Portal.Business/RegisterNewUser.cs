using Portal.Interfaces;

namespace Portal.Business
{
    public class RegisterNewUser
    {
        private readonly ICheckIfCardIsAttachedToUser _checkIfCardIsAttachedToUser;
        private readonly ISaveCardToUser _saveCardToUser;

        public RegisterNewUser(ICheckIfCardIsAttachedToUser checkIfCardIsAttachedToUser, ISaveCardToUser saveCardToUser)
        {
            _checkIfCardIsAttachedToUser = checkIfCardIsAttachedToUser;
            _saveCardToUser = saveCardToUser;
        }

        public bool RegisterNewCardToUser(string cardId, string employeeId)
        {
            if (_checkIfCardIsAttachedToUser.CheckIfCardIsAvailible(cardId))
            {
                //This means the card is not registered to a user already and can be saved to current user
                return _saveCardToUser.CreateNewUserWithCard(cardId, employeeId);
            }

            //If failed this means the card is in use
            return false;
        }
    }
}