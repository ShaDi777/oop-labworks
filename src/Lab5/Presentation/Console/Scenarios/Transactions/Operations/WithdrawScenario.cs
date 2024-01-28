using Core.Contracts.Facades;
using Core.Models;
using Spectre.Console;

namespace Console.Scenarios.Transactions.Operations;

public class WithdrawScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public WithdrawScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Withdraw";

    public async Task Run()
    {
        TextPrompt<decimal> amountPrompt = new TextPrompt<decimal>("How much do you withdraw:")
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

        OperationResult result = await _facade.Withdraw(amount);

        AnsiConsole.WriteLine(result.ToString());
        AnsiConsole.Ask<string>("Ok");
    }
}