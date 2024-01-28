using System.Globalization;
using Core.Contracts.Facades;
using Core.Models.Transactions;
using Spectre.Console;

namespace Console.Scenarios.Transactions.Operations;

public class ShowHistoryScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public ShowHistoryScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Show transaction history";

    public async Task Run()
    {
        ArgumentNullException.ThrowIfNull(_facade.State.Card);
        IEnumerable<Transaction> transactions = await _facade.GetAllTransactions(_facade.State.Card.CardNumber);

        var table = new Table();
        table.AddColumn(new TableColumn("Action").Centered());
        table.AddColumn(new TableColumn("Amount").Centered());
        table.AddColumn(new TableColumn("Date").Centered());
        foreach (Transaction transaction in transactions)
        {
            table.AddRow(
                transaction.TransactionType.ToString(),
                transaction.Value.ToString(CultureInfo.InvariantCulture),
                transaction.TimeStamp.ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.Write(table);
        AnsiConsole.Ask<string>("Ok");
    }
}