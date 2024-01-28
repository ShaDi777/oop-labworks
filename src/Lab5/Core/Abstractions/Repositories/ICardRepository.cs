using Core.Models.Cards;
using Core.Models.Users;

namespace Core.Abstractions.Repositories;

public interface ICardRepository
{
    Task<long> CreateCard(Card card, int pinCode);
    Task<Card?> FindCardByNumber(long cardNumber);
    Task<Card?> ValidateCard(long cardNumber, int pinCode);
    Task<IEnumerable<Card>> FindCardsByUser(User user);
    Task UpdateCard(Card card);
    Task<Card?> DeleteCardByNumber(long cardNumber);
}