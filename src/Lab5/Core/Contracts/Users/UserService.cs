using Core.Abstractions.Repositories;
using Core.Contracts.CurrentState;
using Core.Models.Users;

namespace Core.Contracts.Users;

internal class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly CurrentStateManager _currentStateManager;

    public UserService(IUserRepository repository, CurrentStateManager currentStateManager)
    {
        _repository = repository;
        _currentStateManager = currentStateManager;
    }

    public async Task<bool> CreateNewUser(string name, string password, UserRole role)
    {
        return await _repository.CreateUser(new User(-1, name, role), password);
    }

    public async Task<bool> Login(string name, string password)
    {
        User? user = await _repository.FindUserByNameWithPassword(name, password);

        if (user is null)
        {
            return false;
        }

        _currentStateManager.User = user;
        return true;
    }

    public void Logout()
    {
        _currentStateManager.User = null;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _repository.GetAllUsers();
    }

    public async Task UpdateUser(User user)
    {
        await _repository.UpdateUser(user);
    }

    public async Task<User?> DeleteUser(User user)
    {
        return await _repository.DeleteUserById(user.Id);
    }
}