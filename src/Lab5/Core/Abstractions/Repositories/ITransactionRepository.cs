using Core.Models.Transactions;

namespace Core.Abstractions.Repositories;

public interface ITransactionRepository
{
    Task<bool> CreateTransaction(Transaction transaction);
    Task<IEnumerable<Transaction>> FindAllTransactionsByCardNumber(long cardNumber);
    Task DeleteTransactionsForCardNumber(long cardNumber);
}