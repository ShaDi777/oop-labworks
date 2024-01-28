using System.Diagnostics.CodeAnalysis;
using Core.Contracts.Facades;

namespace Console.Scenarios.Transactions;

public class TransactionManagerScenarioProvider : IScenarioProvider
{
    private readonly IApplicationFacade _facade;
    private readonly IEnumerable<IScenario> _scenarios;

    public TransactionManagerScenarioProvider(
        IApplicationFacade facade,
        IEnumerable<IScenario> scenarios)
    {
        _facade = facade;
        _scenarios = scenarios;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_facade.State.Card is null)
        {
            scenario = null;
            return false;
        }

        scenario = new TransactionManagerScenario(_facade, _scenarios);
        return true;
    }
}