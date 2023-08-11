using HomeBankingMinHub.Models;
using System.Collections.Generic;

namespace HomeBankingMinHub.Repositories
{
    public interface ICardRepository
    {
        Card FindById(long id);

        IEnumerable<Card> GetAllCards();
        IEnumerable<Card> GetCardsByClient(long clientId);

        void Save(Card card);

    }
}
