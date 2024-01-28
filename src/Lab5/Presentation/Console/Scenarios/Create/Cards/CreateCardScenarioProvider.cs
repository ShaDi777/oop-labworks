using System.Diagnostics.CodeAnalysis;
using Core.Contracts.Facades;

namespace Console.Scenarios.Create.Cards;

public class CreateCardScenarioProvider : IScenarioProvider
{
    private readonly IApplicationFacade _facade;

    public CreateCardScenarioProvider(IApplicationFacade facade)
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

        scenario = new CreateCardScenario(_facade);
        return true;
    }
}