using System.Globalization;
using System.Text.RegularExpressions;
using Core.Contracts.Facades;
using Core.Models;

using Spectre.Console;

namespace Console.Scenarios.Create.Cards;

public class CreateCardScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public CreateCardScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Create new card";

    public async Task Run()
    {
        AnsiConsole.WriteLine("Your card number will be generated automatically");
        string pinCode = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your pin-code:")
                .Validate(pin => Regex.IsMatch(pin, "\\d{4}", RegexOptions.IgnoreCase)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Your pin must contain 4 digits[/]")).Secret());

        OperationResult result = await _facade.OpenNewCard(int.Parse(pinCode, CultureInfo.InvariantCulture));

        AnsiConsole.WriteLine(result.ToString());
        AnsiConsole.Ask<string>("Ok");
    }
}