using Core.Abstractions.Repositories;
using Core.Contracts.Cards;
using Core.Models.Cards;
using Core.Models.Transactions;

namespace Core.Contracts.Transactions;

internal class TransactionService : ITransactionService
{
    private ITransactionRepository _repository;
    private ICardService _cardService;

    public TransactionService(ITransactionRepository repository, ICardService cardService)
    {
        _repository = repository;
        _cardService = cardService;
    }

    public async Task<bool> Deposit(Card card, decimal amount)
    {
        if (amount <= 0 || card is null) return false;

        card.Balance += amount;
        await _cardService.UpdateCard(card);
        await _repository.CreateTransaction(
            new Transaction(
                card.CardNumber,
                TransactionType.Deposit,
                amount,
                DateTime.Now));
        return true;
    }

    public async Task<bool> Withdraw(Card card, decimal amount)
    {
        if (card is null || amount <= 0 || card.Balance - amount < 0) return false;

        card.Balance -= amount;
        await _cardService.UpdateCard(card);
        await _repository.CreateTransaction(
            new Transaction(
                card.CardNumber,
                TransactionType.Withdraw,
                -amount,
                DateTime.Now));
        return true;
    }

    public async Task<bool> Transfer(Card cardFrom, Card cardTo, decimal amount)
    {
        if (cardFrom is null || cardTo is null || amount <= 0 || cardFrom.Balance - amount < 0)
            return false;

        cardFrom.Balance -= amount;
        cardTo.Balance += amount;
        await _cardService.UpdateCard(cardFrom);
        await _cardService.UpdateCard(cardTo);
        await _repository.CreateTransaction(
            new Transaction(
                cardFrom.CardNumber,
                TransactionType.Transfer,
                -amount,
                DateTime.Now));
        await _repository.CreateTransaction(
            new Transaction(
                cardTo.CardNumber,
                TransactionType.Transfer,
                amount,
                DateTime.Now));
        return true;
    }

    public async Task ClearTransactionsForCard(Card card)
    {
        await _repository.DeleteTransactionsForCardNumber(card.CardNumber);
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactions(long cardNumber)
    {
        return await _repository.FindAllTransactionsByCardNumber(cardNumber);
    }
}