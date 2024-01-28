using System.Diagnostics.CodeAnalysis;
using Core.Abstractions.Repositories;
using Core.Models.Transactions;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace DataAccess.Repositories;

[SuppressMessage("Connection", "CA2000", Justification = "Disposed by 'using'")]
public class TransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<bool> CreateTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        const string sql = """
                           INSERT INTO transactions
                           VALUES(DEFAULT, :cardNumber, :transactionType, :amount, :timeStamp);
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("cardNumber", transaction.CardNumber)
            .AddParameter("transactionType", transaction.TransactionType)
            .AddParameter("amount", transaction.Value)
            .AddParameter("timeStamp", transaction.TimeStamp);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<IEnumerable<Transaction>> FindAllTransactionsByCardNumber(long cardNumber)
    {
        const string sql = """
                           select card_number, transaction_type, amount, time_stamp
                           from transactions
                           where card_number = :cardNumber;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("cardNumber", cardNumber);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var transactions = new List<Transaction>();
        while (await reader.ReadAsync())
        {
            transactions.Add(new Transaction(
                reader.GetInt64(0),
                await reader.GetFieldValueAsync<TransactionType>(1),
                reader.GetDecimal(2),
                reader.GetDateTime(3)));
        }

        return transactions;
    }

    public async Task DeleteTransactionsForCardNumber(long cardNumber)
    {
        const string sql = """
                           DELETE FROM transactions
                           WHERE card_number = :cardNumber;
                           """;

        NpgsqlConnection connection = await GetConnection();

        await using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("cardNumber", cardNumber);

        await command.ExecuteNonQueryAsync();
    }

    private async Task<NpgsqlConnection> GetConnection()
    {
        return await _connectionProvider.GetConnectionAsync(CancellationToken.None);
    }
}