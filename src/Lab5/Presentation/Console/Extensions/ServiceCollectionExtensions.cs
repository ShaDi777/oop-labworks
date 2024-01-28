using Console.Scenarios.Create.Cards;
using Console.Scenarios.Create.Users;
using Console.Scenarios.Delete.Cards;
using Console.Scenarios.Delete.Users;
using Console.Scenarios.Login.Cards;
using Console.Scenarios.Login.Users;
using Console.Scenarios.Logout.Cards;
using Console.Scenarios.Logout.Users;
using Console.Scenarios.Transactions;
using Console.Scenarios.Transactions.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, UserLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CardLoginScenarioProvider>();

        collection.AddScoped<IScenarioProvider, TransactionManagerScenarioProvider>();

        collection.AddScoped<IScenarioProvider, CreateUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateCardScenarioProvider>();

        collection.AddScoped<IScenarioProvider, UserDeleteScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CardDeleteScenarioProvider>();

        collection.AddScoped<IScenarioProvider, UserLogoutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CardLogoutScenarioProvider>();

        return collection;
    }

    public static IServiceCollection AddTransactionTypes(this IServiceCollection collection)
    {
        collection.AddScoped<IScenario, DepositScenario>();
        collection.AddScoped<IScenario, WithdrawScenario>();
        collection.AddScoped<IScenario, TransferScenario>();
        collection.AddScoped<IScenario, ShowHistoryScenario>();
        collection.AddScoped<IScenario, DeleteScenario>();

        return collection;
    }
}