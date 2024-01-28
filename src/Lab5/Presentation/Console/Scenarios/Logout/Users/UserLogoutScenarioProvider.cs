using System.Diagnostics.CodeAnalysis;
using Core.Contracts.Facades;

namespace Console.Scenarios.Logout.Users;

public class UserLogoutScenarioProvider : IScenarioProvider
{
    private readonly IApplicationFacade _facade;

    public UserLogoutScenarioProvider(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_facade.State.User is null)
        {
            scenario = null;
            return false;
        }

        scenario = new UserLogoutScenario(_facade);
        return true;
    }
}