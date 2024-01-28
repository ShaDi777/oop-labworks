using Core.Contracts.Facades;
using Core.Models;
using Core.Models.Users;
using Spectre.Console;

namespace Console.Scenarios.Create.Users;

public class CreateUserScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public CreateUserScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Create new user";

    public async Task Run()
    {
        TextPrompt<UserRole> rolePrompt = new TextPrompt<UserRole>("Choose user role:")
            .AddChoices(new List<UserRole>
            {
                UserRole.Admin,
                UserRole.User,
            });
        UserRole role = AnsiConsole.Prompt(rolePrompt);

        string name = AnsiConsole.Ask<string>("Enter user name:");
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());

        OperationResult result = await _facade.CreateNewUser(name, password, role);

        AnsiConsole.WriteLine(result.ToString());
        AnsiConsole.Ask<string>("Ok");
    }
}