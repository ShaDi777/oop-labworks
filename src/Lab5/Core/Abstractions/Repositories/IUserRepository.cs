using Core.Models.Users;

namespace Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<bool> CreateUser(User user, string password);
    Task<User?> FindUserByNameWithPassword(string name, string password);
    Task<IEnumerable<User>> GetAllUsers();
    Task<bool> UpdateUser(User updatedUser);
    Task<User?> DeleteUserById(long userId);
}