using System.Diagnostics.CodeAnalysis;
using Core.Abstractions.Repositories;
using Core.Models.Cards;
using Core.Models.Users;
using DataAccess.Hashing;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace DataAccess.Repositories;

[SuppressMessage("Connection", "CA2000", Justification = "Disposed by 'using'")]
public class CardRepository : ICardRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public CardRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<long> CreateCard(Card card, int pinCode)
    {
        ArgumentNullException.ThrowIfNull(card);
        string salt = HashingService.GetNewSalt();
        string pinCodeHash = HashingService.GenerateHash(salt, pinCode);

        const string sql = """
                           INSERT INTO cards
                           VALUES(DEFAULT, :salt, :pinCodeHash, :ownerId, :balance)
                           RETURNING card_number;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("salt", salt)
            .AddParameter("pinCodeHash", pinCodeHash)
            .AddParameter("ownerId", card.OwnerId)
            .AddParameter("balance", card.Balance);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync() is false)
            return -1;

        return reader.GetInt64(0);
    }

    public async Task<Card?> FindCardByNumber(long cardNumber)
    {
        const string sql = """
                           select card_number, owner_user_id, balance
                           from cards
                           where card_number = :cardNumber;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("cardNumber", cardNumber);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync() is false)
            return null;

        return new Card(
            CardNumber: reader.GetInt64(0),
            OwnerId: reader.GetInt64(1))
        {
            Balance = reader.GetDecimal(2),
        };
    }

    public async Task<Card?> ValidateCard(long cardNumber, int pinCode)
    {
        const string sql = """
                           select card_number, owner_user_id, balance, salt, pin_code_hash
                           from cards
                           where card_number = :cardNumber;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("cardNumber", cardNumber);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync() is false)
            return null;

        var card = new Card(
            CardNumber: reader.GetInt64(0),
            OwnerId: reader.GetInt64(1))
        {
            Balance = reader.GetDecimal(2),
        };

        string salt = reader.GetString(3);
        string targetHash = reader.GetString(4);
        string pinCodeHash = HashingService.GenerateHash(salt, pinCode);

        return targetHash.Equals(pinCodeHash, StringComparison.OrdinalIgnoreCase) ? card : null;
    }

    public async Task<IEnumerable<Card>> FindCardsByUser(User user)
    {
        const string sql = """
                           select card_number, owner_user_id, balance
                           from cards
                           where owner_user_id = :user_id;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("user_id", user?.Id ?? -1);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var cards = new List<Card>();
        while (await reader.ReadAsync())
        {
            cards.Add(new Card(
                CardNumber: reader.GetInt64(0),
                OwnerId: reader.GetInt64(1))
            {
                Balance = reader.GetDecimal(2),
            });
        }

        return cards;
    }

    public async Task UpdateCard(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);

        const string sql = """
                           UPDATE cards
                           SET balance = :newBalance, owner_user_id = :newOwnerId
                           WHERE card_number = :cardNumber;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("cardNumber", card.CardNumber)
            .AddParameter("newBalance", card.Balance)
            .AddParameter("newOwnerId", card.OwnerId);

        int rowsAffected = await command.ExecuteNonQueryAsync();
    }

    public async Task<Card?> DeleteCardByNumber(long cardNumber)
    {
        const string sql = """
                           DELETE FROM cards 
                           WHERE card_number = :cardNumber
                           RETURNING card_number, owner_user_id, balance;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("cardNumber", cardNumber);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync() is false)
            return null;

        return new Card(
            CardNumber: reader.GetInt64(0),
            OwnerId: reader.GetInt64(1))
        {
            Balance = reader.GetDecimal(2),
        };
    }

    private async Task<NpgsqlConnection> GetConnection()
    {
        return await _connectionProvider.GetConnectionAsync(CancellationToken.None);
    }
}