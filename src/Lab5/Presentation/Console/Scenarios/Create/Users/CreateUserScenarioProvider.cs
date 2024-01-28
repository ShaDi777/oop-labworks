using System.Diagnostics.CodeAnalysis;
using Core.Contracts.Facades;
using Core.Models.Users;

namespace Console.Scenarios.Create.Users;

public class CreateUserScenarioProvider : IScenarioProvider
{
    private readonly IApplicationFacade _facade;

    public CreateUserScenarioProvider(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_facade.State.User is null || _facade.State.User.UserRole != UserRole.Admin)
        {
            scenario = null;
            return false;
        }

        scenario = new CreateUserScenario(_facade);
        return true;
    }
}