using Core.Contracts.CurrentState;
using Core.Models;
using Core.Models.Transactions;
using Core.Models.Users;

namespace Core.Contracts.Facades;

public interface IApplicationFacade
{
    ICurrentStateService State { get; }

    Task<OperationResult> CreateNewUser(string name, string password, UserRole role);
    Task<OperationResult> OpenNewCard(int pinCode);

    Task<OperationResult> LoginUser(string name, string password);
    Task<OperationResult> LoginCard(long cardNumber, int pinCode);

    OperationResult LogoutUser();
    OperationResult LogoutCard();

    Task<OperationResult> Deposit(decimal amount);
    Task<OperationResult> Withdraw(decimal amount);
    Task<OperationResult> Transfer(long cardNumberTo, decimal amount);

    Task<OperationResult> DeleteUser(User user);
    Task<OperationResult> DeleteCard(long cardNumber);

    Task<IEnumerable<long>> GetUserCardNumbers(User user);
    Task<IEnumerable<User>> GetAllUsers();
    Task<IEnumerable<Transaction>> GetAllTransactions(long cardNumber);
}