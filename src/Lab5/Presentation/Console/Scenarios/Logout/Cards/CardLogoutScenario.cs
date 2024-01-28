using Core.Contracts.Facades;

namespace Console.Scenarios.Logout.Cards;

public class CardLogoutScenario : IScenario
{
    private readonly IApplicationFacade _facade;

    public CardLogoutScenario(IApplicationFacade facade)
    {
        _facade = facade;
    }

    public string Name => "Logout card";

    public Task Run()
    {
        _facade.LogoutCard();
        return Task.CompletedTask;
    }
}