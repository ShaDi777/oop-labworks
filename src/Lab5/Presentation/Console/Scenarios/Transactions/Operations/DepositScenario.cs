using Core.Contracts.Facades;
using Core.Models;
using Spectre.Console;

namespace Console.Scenarios.Transactions.Operations;

public class DepositScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public DepositScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Deposit";

    public async Task Run()
    {
        TextPrompt<decimal> amountPrompt = new TextPrompt<decimal>("How much do you deposit:")
            .ValidationErrorMessage("[red]That's not a valid amount[/]")
            .Validate(value =>
            {
                return value switch
                {
                    <= 0 => ValidationResult.Error("[red]Amount must be greater than 0[/]"),
                    _ => ValidationResult.Success(),
                };
            });
        decimal amount = AnsiConsole.Prompt(amountPrompt);

        OperationResult result = await _facade.Deposit(amount);

        AnsiConsole.WriteLine(result.ToString());
        AnsiConsole.Ask<string>("Ok");
    }
}