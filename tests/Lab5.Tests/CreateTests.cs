using System.Threading.Tasks;
using Core.Abstractions.Repositories;
using Core.Contracts.Cards;
using Core.Contracts.CurrentState;
using Core.Contracts.Facades;
using Core.Contracts.Transactions;
using Core.Contracts.Users;
using Core.Models;
using Core.Models.Cards;
using Core.Models.Users;
using Moq;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class CreateTests
{
    private IApplicationFacade _facade;
    private UserService _userService;
    private CardService _cardService;
    private TransactionService _transactionService;

    private CurrentStateManager _currentState = new CurrentStateManager();

    private Mock<IUserRepository> _userRepositoryMock = new();
    private Mock<ITransactionRepository> _transactionRepositoryMock = new();
    private Mock<ICardRepository> _cardRepositoryMock = new();

    public CreateTests()
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
    public async Task CreateNewUserByAdmin()
    {
        // Arrange
        const string name = "name";
        const string password = "password";
        const UserRole role = UserRole.Admin;
        _userRepositoryMock.Setup(m => m.CreateUser(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
        _currentState.User = new User(1, "Admin", UserRole.Admin);

        // Act
        OperationResult result = await _facade.CreateNewUser(name, password, role);

        // Assert
        Assert.Equal(OperationResult.Success, result);
        _userRepositoryMock.Verify(
            mock =>
                mock.CreateUser(
                    It.Is<User>(user =>
                        user.Name == name &&
                        user.UserRole == role),
                    password),
            Times.Once);
    }

    [Fact]
    public async Task CreateNewUserByUser()
    {
        // Arrange
        const string name = "name";
        const string password = "password";
        const UserRole role = UserRole.User;

        _currentState.User = new User(1, "User", UserRole.User);

        // Act
        OperationResult result = await _facade.CreateNewUser(name, password, role);

        // Assert
        Assert.Equal(OperationResult.AccessDenied, result);
        _userRepositoryMock.Verify(
            mock =>
                mock.CreateUser(
                    It.Is<User>(user =>
                        user.Name == name &&
                        user.UserRole == role),
                    password),
            Times.Never);
    }

    [Fact]
    public async Task CreateNewCard()
    {
        // Arrange
        const int pinCode = 1234;
        const long ownerId = 1234;

        _currentState.User = new User(ownerId, "User", UserRole.User);

        // Act
        OperationResult result = await _facade.OpenNewCard(pinCode);

        // Assert
        Assert.Equal(OperationResult.Success, result);
        _cardRepositoryMock.Verify(
            mock =>
                mock.CreateCard(
                    It.Is<Card>(c =>
                        c.Balance == 0 &&
                        c.OwnerId == ownerId),
                    pinCode),
            Times.Once);
    }
}