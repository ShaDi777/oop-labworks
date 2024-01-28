using System;
using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.Displays;

public class DisplayBuilder
{
    private ILogger? _logger;
    private IDisplayDriver? _driver;

    public DisplayBuilder WithLogger(ILogger logger)
    {
        _logger = logger;
        return this;
    }

    public DisplayBuilder WithDriver(IDisplayDriver driver)
    {
        _driver = driver;
        return this;
    }

    public Display Build()
    {
        ArgumentNullException.ThrowIfNull(_logger);
        ArgumentNullException.ThrowIfNull(_driver);

        return new Display(_logger, _driver);
    }
}