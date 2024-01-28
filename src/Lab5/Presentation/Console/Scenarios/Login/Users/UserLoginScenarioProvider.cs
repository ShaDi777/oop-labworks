using System.Diagnostics.CodeAnalysis;
using Core.Contracts.Facades;

namespace Console.Scenarios.Login.Users;

public class UserLoginScenarioProvider : IScenarioProvider
{
    private readonly IApplicationFacade _facade;

    public UserLoginScenarioProvider(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_facade.State.User is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new UserLoginScenario(_facade);
        return true;
    }
}