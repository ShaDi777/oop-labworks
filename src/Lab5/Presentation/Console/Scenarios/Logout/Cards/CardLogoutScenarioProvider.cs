using System.Diagnostics.CodeAnalysis;
using Core.Contracts.Facades;

namespace Console.Scenarios.Logout.Cards;

public class CardLogoutScenarioProvider : IScenarioProvider
{
    private readonly IApplicationFacade _facade;

    public CardLogoutScenarioProvider(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_facade.State.Card is null)
        {
            scenario = null;
            return false;
        }

        scenario = new CardLogoutScenario(_facade);
        return true;
    }
}