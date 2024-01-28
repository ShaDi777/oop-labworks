using System.Diagnostics.CodeAnalysis;
using Core.Contracts.Facades;
using Core.Models.Users;

namespace Console.Scenarios.Delete.Users;

public class UserDeleteScenarioProvider : IScenarioProvider
{
    private readonly IApplicationFacade _facade;

    public UserDeleteScenarioProvider(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_facade.State.User is null || _facade.State.User.UserRole != UserRole.Admin)
        {
            scenario = null;
            return false;
        }

        scenario = new UserDeleteScenario(_facade);
        return true;
    }
}