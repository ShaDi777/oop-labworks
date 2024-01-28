using Core.Models.Cards;
using Core.Models.Transactions;

namespace Core.Contracts.Transactions;

public interface ITransactionService
{
    Task<bool> Deposit(Card card, decimal amount);
    Task<bool> Withdraw(Card card, decimal amount);
    Task<bool> Transfer(Card cardFrom, Card cardTo, decimal amount);
    Task ClearTransactionsForCard(Card card);
    Task<IEnumerable<Transaction>> GetAllTransactions(long cardNumber);
}