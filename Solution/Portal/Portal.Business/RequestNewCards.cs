using AutoMapper;
using Portal.DataAccess;
using System.Collections.Generic;
using NewCards = Portal.Business.Models.NewCards;

namespace Portal.Business
{
    public class RequestNewCards
    {
        private readonly GetNewCards _getNewCards;

        public RequestNewCards(GetNewCards getNewCards)
        {
            _getNewCards = getNewCards;
        }

        public IEnumerable<NewCards> RequstAllNewCards()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataAccess.Models.NewCards, NewCards>());

            var mapper = mapperConfig.CreateMapper();

            return mapper.Map<IEnumerable<NewCards>>(_getNewCards.GetAllNewCards());
        }
    }
}