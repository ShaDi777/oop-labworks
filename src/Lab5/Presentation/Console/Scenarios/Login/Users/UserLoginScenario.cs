using Core.Contracts.Facades;
using Core.Models;
using Spectre.Console;

namespace Console.Scenarios.Login.Users;

public class UserLoginScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public UserLoginScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Login";

    public async Task Run()
    {
        string name = AnsiConsole.Ask<string>("Enter your name:");
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your password:").Secret());

        OperationResult result = await _facade.LoginUser(name, password);

        string message = result == OperationResult.Success
            ? "Successful login"
            : "Incorrect name or password";

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}