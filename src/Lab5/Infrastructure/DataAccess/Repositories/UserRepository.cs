using System.Diagnostics.CodeAnalysis;
using Core.Abstractions.Repositories;
using Core.Models.Users;
using DataAccess.Hashing;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace DataAccess.Repositories;

[SuppressMessage("Connection", "CA2000", Justification = "Disposed by 'using'")]
public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<bool> CreateUser(User user, string password)
    {
        ArgumentNullException.ThrowIfNull(user);
        string salt = HashingService.GetNewSalt();
        string passwordHash = HashingService.GenerateHash(salt, password);

        const string sql = """
                           INSERT INTO users
                           VALUES(DEFAULT, :name, :salt, :passwordHash, :role);
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("name", user.Name)
            .AddParameter("salt", salt)
            .AddParameter("passwordHash", passwordHash)
            .AddParameter("role", user.UserRole);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<User?> FindUserByNameWithPassword(string name, string password)
    {
        const string sql = """
                           select id, name, role, salt, password_hash
                           from users
                           where name = :name;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("name", name);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync() is false)
            return null;

        var user = new User(
            Id: reader.GetInt64(0),
            Name: reader.GetString(1),
            UserRole: await reader.GetFieldValueAsync<UserRole>(2));

        string salt = reader.GetString(3);
        string targetHash = reader.GetString(4);
        string passwordHash = HashingService.GenerateHash(salt, password);

        return targetHash.Equals(passwordHash, StringComparison.OrdinalIgnoreCase) ? user : null;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        const string sql = """
                           select id, name, role
                           from users;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using var command = new NpgsqlCommand(sql, connection);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var users = new List<User>();
        while (await reader.ReadAsync())
        {
            users.Add(new User(
                Id: reader.GetInt64(0),
                Name: reader.GetString(1),
                UserRole: await reader.GetFieldValueAsync<UserRole>(2)));
        }

        return users;
    }

    public async Task<bool> UpdateUser(User updatedUser)
    {
        ArgumentNullException.ThrowIfNull(updatedUser);

        const string sql = """
                           UPDATE users
                           SET name = :name, role = :role
                           WHERE id = :userId;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userId", updatedUser.Id)
            .AddParameter("name", updatedUser.Name)
            .AddParameter("role", updatedUser.UserRole);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<User?> DeleteUserById(long userId)
    {
        const string sql = """
                           DELETE FROM users
                           WHERE id = :userId
                           RETURNING id, name, role;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userId", userId);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync() is false)
            return null;

        return new User(
            Id: reader.GetInt64(0),
            Name: reader.GetString(1),
            UserRole: await reader.GetFieldValueAsync<UserRole>(2));
    }

    private async Task<NpgsqlConnection> GetConnection()
    {
        return await _connectionProvider.GetConnectionAsync(CancellationToken.None);
    }
}