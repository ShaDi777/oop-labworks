using System.Diagnostics.CodeAnalysis;
using Core.Contracts.Facades;

namespace Console.Scenarios.Login.Cards;

public class CardLoginScenarioProvider : IScenarioProvider
{
    private readonly IApplicationFacade _facade;

    public CardLoginScenarioProvider(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_facade.State.User is null || _facade.State.Card is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new CardLoginScenario(_facade);
        return true;
    }
}