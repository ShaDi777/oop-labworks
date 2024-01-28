using Core.Contracts.Facades;
using Spectre.Console;

namespace Console.Scenarios.Transactions;

public class TransactionManagerScenario : IScenario
{
    private readonly IApplicationFacade _facade;
    private readonly IEnumerable<IScenario> _scenarios;

    public TransactionManagerScenario(IApplicationFacade facade, IEnumerable<IScenario> scenarios)
    {
        _facade = facade;
        _scenarios = scenarios;
    }

    public string Name => "Manage card";

    public Task Run()
    {
        ArgumentNullException.ThrowIfNull(_facade.State.Card);
        AnsiConsole.WriteLine("Selected card: " + _facade.State.Card.CardNumber);
        AnsiConsole.WriteLine("Balance: " + _facade.State.Card.Balance);
        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .Title("Select action")
            .AddChoices(_scenarios)
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);
        scenario.Run();
        return Task.CompletedTask;
    }
}