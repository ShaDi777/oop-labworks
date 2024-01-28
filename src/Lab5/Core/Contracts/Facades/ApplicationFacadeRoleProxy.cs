using Core.Contracts.CurrentState;
using Core.Models;
using Core.Models.Transactions;
using Core.Models.Users;

namespace Core.Contracts.Facades;

public class ApplicationFacadeRoleProxy : IApplicationFacade
{
    private readonly IApplicationFacade _facade;

    public ApplicationFacadeRoleProxy(ApplicationFacade facade)
    {
        _facade = facade;
    }

    public ICurrentStateService State => _facade.State;

    public Task<OperationResult> CreateNewUser(string name, string password, UserRole role)
    {
        if (State.User is null || State.User.UserRole != UserRole.Admin)
        {
            return Task.FromResult(OperationResult.AccessDenied);
        }

        return _facade.CreateNewUser(name, password, role);
    }

    public Task<OperationResult> OpenNewCard(int pinCode)
    {
        return _facade.OpenNewCard(pinCode);
    }

    public Task<OperationResult> LoginUser(string name, string password)
    {
        return _facade.LoginUser(name, password);
    }

    public async Task<OperationResult> LoginCard(long cardNumber, int pinCode)
    {
        return await _facade.LoginCard(cardNumber, pinCode);
    }

    public OperationResult LogoutUser()
    {
        return _facade.LogoutUser();
    }

    public OperationResult LogoutCard()
    {
        return _facade.LogoutCard();
    }

    public Task<OperationResult> Deposit(decimal amount)
    {
        return _facade.Deposit(amount);
    }

    public Task<OperationResult> Withdraw(decimal amount)
    {
        return _facade.Withdraw(amount);
    }

    public Task<OperationResult> Transfer(long cardNumberTo, decimal amount)
    {
        return _facade.Transfer(cardNumberTo, amount);
    }

    public Task<OperationResult> DeleteUser(User user)
    {
        if (State.User is null || State.User.UserRole != UserRole.Admin || State.User == user)
        {
            return Task.FromResult(OperationResult.AccessDenied);
        }

        return _facade.DeleteUser(user);
    }

    public Task<OperationResult> DeleteCard(long cardNumber)
    {
        if (State.User is null || State.User.UserRole != UserRole.Admin)
        {
            return Task.FromResult(OperationResult.AccessDenied);
        }

        return _facade.DeleteCard(cardNumber);
    }

    public Task<IEnumerable<long>> GetUserCardNumbers(User user)
    {
        if (State.User is null || (State.User.UserRole != UserRole.Admin && State.User != user))
        {
            return Task.FromResult(Enumerable.Empty<long>());
        }

        return _facade.GetUserCardNumbers(user);
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        if (State.User is null || State.User.UserRole != UserRole.Admin)
        {
            return Task.FromResult(Enumerable.Empty<User>());
        }

        return _facade.GetAllUsers();
    }

    public Task<IEnumerable<Transaction>> GetAllTransactions(long cardNumber)
    {
        return _facade.GetAllTransactions(cardNumber);
    }
}