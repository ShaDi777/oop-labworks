using Core.Contracts.Facades;
using Core.Models;
using Spectre.Console;

namespace Console.Scenarios.Transactions.Operations;

public class DeleteScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public DeleteScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Delete this card";

    public async Task Run()
    {
        ArgumentNullException.ThrowIfNull(_facade.State.Card);
        bool confirm = AnsiConsole.Confirm("Are you sure?");
        if (confirm)
        {
            OperationResult result = await _facade.DeleteCard(_facade.State.Card.CardNumber);
            AnsiConsole.WriteLine("Deleting card...");
            AnsiConsole.WriteLine(result.ToString());
            if (result == OperationResult.Success)
            {
                _facade.LogoutCard();
            }
        }

        AnsiConsole.Ask<string>("Ok");
    }
}