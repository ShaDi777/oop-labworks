using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using Core.Contracts.Facades;
using Core.Models;
using Spectre.Console;

namespace Console.Scenarios.Login.Cards;

[SuppressMessage("ToString", "CA1305", Justification = "long conversion to string")]
public class CardLoginScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public CardLoginScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Choose card";

    public async Task Run()
    {
        ArgumentNullException.ThrowIfNull(_facade.State.User);

        var cardNumbers = (await _facade.GetUserCardNumbers(_facade.State.User)).ToList();

        if (cardNumbers.Count == 0)
        {
            AnsiConsole.WriteLine("You do not have any cards");
            AnsiConsole.Ask<string>("Ok");
            return;
        }

        SelectionPrompt<long> selector = new SelectionPrompt<long>()
            .Title("Select card")
            .AddChoices(cardNumbers)
            .UseConverter(x => x.ToString());
        long cardNumber = AnsiConsole.Prompt(selector);

        AnsiConsole.WriteLine("You selected card " + cardNumber);
        string pinCode = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your pin-code:")
                .Validate(pin => Regex.IsMatch(pin, "\\d{4}", RegexOptions.IgnoreCase)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Your pin must contain 4 digits[/]")).Secret());

        OperationResult result = await _facade.LoginCard(cardNumber, int.Parse(pinCode, CultureInfo.InvariantCulture));

        AnsiConsole.WriteLine(result.ToString());
        AnsiConsole.Ask<string>("Ok");
    }
}