using Core.Contracts.Facades;

namespace Console.Scenarios.Logout.Users;

public class UserLogoutScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public UserLogoutScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Logout";

    public Task Run()
    {
        _facade.LogoutCard();
        _facade.LogoutUser();
        return Task.CompletedTask;
    }
}