using Core.Contracts.Cards;
using Core.Contracts.CurrentState;
using Core.Contracts.Transactions;
using Core.Contracts.Users;
using Core.Models;
using Core.Models.Cards;
using Core.Models.Transactions;
using Core.Models.Users;

namespace Core.Contracts.Facades;

public class ApplicationFacade : IApplicationFacade
{
    private IUserService _userService;
    private ICardService _cardService;
    private ITransactionService _transactionService;

    public ApplicationFacade(
        IUserService userService,
        ICardService cardService,
        ITransactionService transactionService,
        ICurrentStateService currentStateManager)
    {
        _userService = userService;
        _cardService = cardService;
        _transactionService = transactionService;
        State = currentStateManager;
    }

    public ICurrentStateService State { get; }

    public async Task<OperationResult> CreateNewUser(string name, string password, UserRole role)
    {
        return await _userService.CreateNewUser(name, password, role)
            ? OperationResult.Success
            : OperationResult.Failure;
    }

    public async Task<OperationResult> OpenNewCard(int pinCode)
    {
        ArgumentNullException.ThrowIfNull(State.User);
        await _cardService.OpenNewCard(State.User, pinCode);
        return OperationResult.Success;
    }

    public async Task<OperationResult> LoginUser(string name, string password)
    {
        return await _userService.Login(name, password)
            ? OperationResult.Success
            : OperationResult.Failure;
    }

    public async Task<OperationResult> LoginCard(long cardNumber, int pinCode)
    {
        return await _cardService.Login(cardNumber, pinCode)
            ? OperationResult.Success
            : OperationResult.Failure;
    }

    public OperationResult LogoutUser()
    {
        _userService.Logout();
        return OperationResult.Success;
    }

    public OperationResult LogoutCard()
    {
        _cardService.Logout();
        return OperationResult.Success;
    }

    public async Task<OperationResult> Deposit(decimal amount)
    {
        ArgumentNullException.ThrowIfNull(State.Card);
        return await _transactionService.Deposit(State.Card, amount)
            ? OperationResult.Success
            : OperationResult.Failure;
    }

    public async Task<OperationResult> Withdraw(decimal amount)
    {
        ArgumentNullException.ThrowIfNull(State.Card);
        return await _transactionService.Withdraw(State.Card, amount)
            ? OperationResult.Success
            : OperationResult.Failure;
    }

    public async Task<OperationResult> Transfer(long cardNumberTo, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(State.Card);
        Card? targetCard = await _cardService.FindCardByNumber(cardNumberTo);
        return targetCard is not null &&
               await _transactionService.Transfer(State.Card, targetCard, amount)
            ? OperationResult.Success
            : OperationResult.Failure;
    }

    public async Task<OperationResult> DeleteUser(User user)
    {
        foreach (long userCardNumber in await GetUserCardNumbers(user))
        {
            await DeleteCard(userCardNumber);
        }

        User? userDeleted = await _userService.DeleteUser(user);
        if (userDeleted is null)
        {
            return OperationResult.Failure;
        }

        return OperationResult.Success;
    }

    public async Task<OperationResult> DeleteCard(long cardNumber)
    {
        await _transactionService.ClearTransactionsForCard(
            await _cardService.FindCardByNumber(cardNumber)
            ?? throw new ArgumentNullException(nameof(cardNumber)));

        Card? card = await _cardService.DeleteCardByNumber(cardNumber);
        if (card is null)
        {
            return OperationResult.Failure;
        }

        return OperationResult.Success;
    }

    public async Task<IEnumerable<long>> GetUserCardNumbers(User user)
    {
        return (await _cardService.FindUserCards(user)).Select(elem => elem.CardNumber);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userService.GetAllUsers();
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactions(long cardNumber)
    {
        return await _transactionService.GetAllTransactions(cardNumber);
    }
}