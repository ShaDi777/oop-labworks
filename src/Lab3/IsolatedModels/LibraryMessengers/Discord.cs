using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.LibraryMessengers;

public class Discord : IDiscordMessenger
{
    private readonly ILogger _logger;

    public Discord(ILogger logger)
    {
        _logger = logger;
    }

    public void PostItem(long chatId, string text)
    {
        _logger.Log("Discord\n" + text);
    }
}