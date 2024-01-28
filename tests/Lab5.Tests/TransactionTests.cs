using System.Threading.Tasks;
using Core.Abstractions.Repositories;
using Core.Contracts.Cards;
using Core.Contracts.CurrentState;
using Core.Contracts.Facades;
using Core.Contracts.Transactions;
using Core.Contracts.Users;
using Core.Models;
using Core.Models.Cards;
using Core.Models.Transactions;
using Core.Models.Users;
using Moq;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class TransactionTests
{
    private IApplicationFacade _facade;
    private UserService _userService;
    private CardService _cardService;
    private TransactionService _transactionService;

    private CurrentStateManager _currentState = new CurrentStateManager();

    private Mock<IUserRepository> _userRepositoryMock = new();
    private Mock<ITransactionRepository> _transactionRepositoryMock = new();
    private Mock<ICardRepository> _cardRepositoryMock = new();

    public TransactionTests()
    {
        _userService = new UserService(_userRepositoryMock.Object, _currentState);
        _cardService = new CardService(_cardRepositoryMock.Object, _currentState);
        _transactionService = new TransactionService(_transactionRepositoryMock.Object, _cardService);

        _facade = new ApplicationFacadeRoleProxy(
            new ApplicationFacade(
                _userService,
                _cardService,
                _transactionService,
                _currentState));
    }

    [Fact]
    public async Task DepositIncreaseBalance()
    {
        // Arrange
        const long cardNumber = 1234;
        const decimal amount = 100;

        _currentState.User = new User(1, "Admin", UserRole.Admin);
        _currentState.Card = new Card(cardNumber, 1) { Balance = 0 };

        // Act
        OperationResult result = await _facade.Deposit(amount);

        // Assert
        Assert.Equal(OperationResult.Success, result);
        _transactionRepositoryMock.Verify(
            mock =>
            mock.CreateTransaction(
                It.Is<Transaction>(transaction =>
                    transaction.CardNumber == cardNumber &&
                    transaction.TransactionType == TransactionType.Deposit &&
                    transaction.Value == amount)),
            Times.Once);
        Assert.Equal(amount, _currentState.Card.Balance);
    }

    [Fact]
    public async Task DepositIncorrectAmount()
    {
        // Arrange
        const long cardNumber = 1234;
        const decimal amount = -100;

        _currentState.User = new User(1, "Admin", UserRole.Admin);
        _currentState.Card = new Card(cardNumber, 1) { Balance = 0 };

        // Act
        OperationResult result = await _facade.Deposit(amount);

        // Assert
        Assert.Equal(OperationResult.Failure, result);
        Assert.Equal(0, _currentState.Card.Balance);
    }

    [Fact]
    public async Task WithdrawDecreaseBalance()
    {
        // Arrange
        const long cardNumber = 1234;
        const decimal startBalance = 100;
        const decimal amount = 10;

        _currentState.User = new User(1, "Admin", UserRole.Admin);
        _currentState.Card = new Card(cardNumber, 1) { Balance = startBalance };

        // Act
        OperationResult result = await _facade.Withdraw(amount);

        // Assert
        Assert.Equal(OperationResult.Success, result);
        _transactionRepositoryMock.Verify(
            mock =>
                mock.CreateTransaction(
                    It.Is<Transaction>(transaction =>
                        transaction.CardNumber == cardNumber &&
                        transaction.TransactionType == TransactionType.Withdraw &&
                        transaction.Value == -amount)),
            Times.Once);
        Assert.Equal(startBalance - amount, _currentState.Card.Balance);
    }

    [Fact]
    public async Task WithdrawNotEnoughBalance()
    {
        // Arrange
        const long cardNumber = 1234;
        const decimal startBalance = 10;
        const decimal amount = 100;

        _currentState.User = new User(1, "Admin", UserRole.Admin);
        _currentState.Card = new Card(cardNumber, 1) { Balance = startBalance };

        // Act
        OperationResult result = await _facade.Withdraw(amount);

        // Assert
        Assert.Equal(OperationResult.Failure, result);
        Assert.Equal(startBalance, _currentState.Card.Balance);
    }

    [Fact]
    public async Task TransferTest()
    {
        // Arrange
        const long cardNumberFrom = 1234;
        const long cardNumberTo = 9999;
        const decimal startBalance = 10;
        const decimal amount = 5;

        var cardFrom = new Card(cardNumberFrom, 1) { Balance = startBalance };
        var cardTo = new Card(cardNumberTo, 2) { Balance = startBalance };
        _cardRepositoryMock.Setup(m => m.FindCardByNumber(cardNumberFrom)).ReturnsAsync(cardFrom);
        _cardRepositoryMock.Setup(m => m.FindCardByNumber(cardNumberTo)).ReturnsAsync(cardTo);

        _currentState.User = new User(1, "Admin", UserRole.Admin);
        _currentState.Card = cardFrom;

        // Act
        OperationResult result = await _facade.Transfer(cardNumberTo, amount);

        // Assert
        Assert.Equal(OperationResult.Success, result);
        Assert.Equal(startBalance - amount, cardFrom.Balance);
        Assert.Equal(startBalance + amount, cardTo.Balance);
        _transactionRepositoryMock.Verify(
            mock =>
                mock.CreateTransaction(
                    It.Is<Transaction>(transaction =>
                        transaction.CardNumber == cardNumberFrom &&
                        transaction.TransactionType == TransactionType.Transfer &&
                        transaction.Value == -amount)),
            Times.Once);
        _transactionRepositoryMock.Verify(
            mock =>
                mock.CreateTransaction(
                    It.Is<Transaction>(transaction =>
                        transaction.CardNumber == cardNumberTo &&
                        transaction.TransactionType == TransactionType.Transfer &&
                        transaction.Value == amount)),
            Times.Once);
    }

    [Fact]
    public async Task TransferNotEnough()
    {
        // Arrange
        const long cardNumberFrom = 1234;
        const long cardNumberTo = 9999;
        const decimal startBalance = 10;
        const decimal amount = 99;

        var cardFrom = new Card(cardNumberFrom, 1) { Balance = startBalance };
        var cardTo = new Card(cardNumberTo, 2) { Balance = startBalance };
        _cardRepositoryMock.Setup(m => m.FindCardByNumber(cardNumberFrom)).ReturnsAsync(cardFrom);
        _cardRepositoryMock.Setup(m => m.FindCardByNumber(cardNumberTo)).ReturnsAsync(cardTo);

        _currentState.User = new User(1, "Admin", UserRole.Admin);
        _currentState.Card = cardFrom;

        // Act
        OperationResult result = await _facade.Transfer(cardNumberTo, amount);

        // Assert
        Assert.Equal(OperationResult.Failure, result);
        Assert.Equal(startBalance, cardFrom.Balance);
        Assert.Equal(startBalance, cardTo.Balance);
    }

    [Fact]
    public async Task TransferReceiverNotExist()
    {
        // Arrange
        const long cardNumberFrom = 1234;
        const decimal startBalance = 10;
        const decimal amount = 5;

        var cardFrom = new Card(cardNumberFrom, 1) { Balance = startBalance };
        _currentState.User = new User(1, "Admin", UserRole.Admin);
        _currentState.Card = cardFrom;

        // Act
        OperationResult result = await _facade.Transfer(9999, amount);

        // Assert
        Assert.Equal(OperationResult.Failure, result);
        Assert.Equal(startBalance, cardFrom.Balance);
    }
}