using System;
using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers;

public class MessengerBuilder
{
    private ILogger? _logger;

    public MessengerBuilder WithLogger(ILogger logger)
    {
        _logger = logger;
        return this;
    }

    public Messenger Build()
    {
        ArgumentNullException.ThrowIfNull(_logger);

        return new Messenger(_logger);
    }
}