using System.Globalization;
using Core.Contracts.Facades;
using Core.Models;
using Core.Models.Users;
using Spectre.Console;

namespace Console.Scenarios.Delete.Cards;

public class CardDeleteScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public CardDeleteScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Delete cards";

    public async Task Run()
    {
        var cards = new List<long>();
        IEnumerable<User> users = await _facade.GetAllUsers();
        foreach (User user in users)
        {
            cards.AddRange(await _facade.GetUserCardNumbers(user));
        }

        SelectionPrompt<long> selector = new SelectionPrompt<long>()
            .Title("Select card that you want to permanently delete")
            .AddChoices(cards)
            .UseConverter(x => x.ToString(CultureInfo.InvariantCulture));

        long selection = AnsiConsole.Prompt(selector);
        bool confirm = AnsiConsole.Confirm("Are you sure you want to delete card " + selection + "?");
        if (confirm)
        {
            OperationResult result = await _facade.DeleteCard(selection);
            AnsiConsole.WriteLine("Deleting card...");
            AnsiConsole.WriteLine(result.ToString());
        }

        AnsiConsole.Ask<string>("Ok");
    }
}