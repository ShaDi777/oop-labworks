using Core.Contracts.Facades;
using Core.Models;
using Spectre.Console;

namespace Console.Scenarios.Transactions.Operations;

public class TransferScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public TransferScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Transfer";

    public async Task Run()
    {
        long cardNumberTo = AnsiConsole.Ask<long>("Enter receiver card number:");
        TextPrompt<decimal> amountPrompt = new TextPrompt<decimal>("How much do you want to send:")
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

        OperationResult result = await _facade.Transfer(cardNumberTo, amount);
        string message = result == OperationResult.Success
            ? "Success"
            : "Probably this card number does not exist";
        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}