using System;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;

public class LoggerProxyAddressee : IAddressee
{
    private readonly IAddressee _addressee;
    private readonly ILogger _logger;

    public LoggerProxyAddressee(IAddressee addressee, ILogger logger)
    {
        _addressee = addressee;
        _logger = logger;
    }

    public void ReceiveMessage(IMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        _logger.Log(message.Title + "\nPriority: " + message.Priority + "\n\n" + message.Text + "\n");
        _addressee.ReceiveMessage(message);
    }
}