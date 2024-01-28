using Core.Contracts.Facades;
using Core.Models;
using Core.Models.Users;
using Spectre.Console;

namespace Console.Scenarios.Delete.Users;

public class UserDeleteScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public UserDeleteScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Delete users";

    public async Task Run()
    {
        IEnumerable<User> users = await _facade.GetAllUsers();

        SelectionPrompt<User> selector = new SelectionPrompt<User>()
            .Title("Select user you want to permanently delete")
            .AddChoices(users)
            .UseConverter(x => x.UserRole + "/" + x.Name);

        User user = AnsiConsole.Prompt(selector);
        bool confirm = AnsiConsole.Confirm("Are you sure you want to delete " + user.UserRole + "/" + user.Name + "?");
        if (confirm)
        {
            OperationResult result = await _facade.DeleteUser(user);
            AnsiConsole.WriteLine("Deleting...");
            AnsiConsole.WriteLine(result.ToString());
        }

        AnsiConsole.Ask<string>("Ok");
    }
}