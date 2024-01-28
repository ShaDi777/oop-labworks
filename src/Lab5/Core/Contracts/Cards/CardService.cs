using Core.Abstractions.Repositories;
using Core.Contracts.CurrentState;
using Core.Models.Cards;
using Core.Models.Users;

namespace Core.Contracts.Cards;

internal class CardService : ICardService
{
    private readonly ICardRepository _repository;
    private readonly CurrentStateManager _currentStateManager;

    public CardService(ICardRepository repository, CurrentStateManager currentStateManager)
    {
        _repository = repository;
        _currentStateManager = currentStateManager;
    }

    public async Task<Card> OpenNewCard(User user, int pinCode)
    {
        long cardId = await _repository.CreateCard(new Card(-1, user.Id), pinCode);
        return new Card(cardId, user.Id);
    }

    public async Task<bool> Login(long cardNumber, int pinCode)
    {
        Card? card = await _repository.ValidateCard(cardNumber, pinCode);

        if (card is null)
        {
            return false;
        }

        _currentStateManager.Card = card;
        return true;
    }

    public void Logout()
    {
        _currentStateManager.Card = null;
    }

    public async Task<Card?> FindCardByNumber(long cardNumber)
    {
        return await _repository.FindCardByNumber(cardNumber);
    }

    public async Task<IEnumerable<Card>> FindUserCards(User user)
    {
        return await _repository.FindCardsByUser(user);
    }

    public async Task UpdateCard(Card card)
    {
        await _repository.UpdateCard(card);
    }

    public async Task<Card?> DeleteCardByNumber(long cardNumber)
    {
        return await _repository.DeleteCardByNumber(cardNumber);
    }
}