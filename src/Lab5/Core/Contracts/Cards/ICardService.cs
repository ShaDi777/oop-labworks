using Core.Models.Cards;
using Core.Models.Users;

namespace Core.Contracts.Cards;

public interface ICardService
{
    Task<Card> OpenNewCard(User user, int pinCode);
    Task<bool> Login(long cardNumber, int pinCode);
    void Logout();
    Task<Card?> FindCardByNumber(long cardNumber);
    Task<IEnumerable<Card>> FindUserCards(User user);
    Task UpdateCard(Card card);
    Task<Card?> DeleteCardByNumber(long cardNumber);
}