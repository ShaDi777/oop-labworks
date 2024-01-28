using Core.Contracts.Cards;
using Core.Contracts.CurrentState;
using Core.Contracts.Facades;
using Core.Contracts.Transactions;
using Core.Contracts.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<ITransactionService, TransactionService>();
        collection.AddScoped<ICardService, CardService>();

        collection.AddScoped<ApplicationFacade, ApplicationFacade>();
        collection.AddScoped<IApplicationFacade, ApplicationFacadeRoleProxy>();

        // collection.AddScoped<CurrentStateManager>();
        // collection.AddScoped<ICurrentStateService>(p => p.GetRequiredService<CurrentStateManager>());
        collection.AddSingleton<ICurrentStateService, CurrentStateManager>();

        return collection;
    }
}