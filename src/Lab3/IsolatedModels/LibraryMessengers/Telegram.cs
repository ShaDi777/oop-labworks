using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.LibraryMessengers;

public class Telegram : ITelegramMessenger
{
    private readonly ILogger _logger;

    public Telegram(ILogger logger)
    {
        _logger = logger;
    }

    public void SendMessage(string apiKey, long userId, string message)
    {
        _logger.Log("Telegram\n" + message);
    }
}