using Core.Models.Users;

namespace Core.Contracts.Users;

public interface IUserService
{
    Task<bool> CreateNewUser(string name, string password, UserRole role);
    Task<bool> Login(string name, string password);
    void Logout();
    Task<IEnumerable<User>> GetAllUsers();
    Task UpdateUser(User user);
    Task<User?> DeleteUser(User user);
}