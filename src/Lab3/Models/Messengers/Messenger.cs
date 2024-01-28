using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers;

public class Messenger : IMessenger
{
    private readonly ILogger _logger;
    private string _text = string.Empty;

    public Messenger(ILogger logger)
    {
        _logger = logger;
    }

    public void PrintText(string text)
    {
        _text += text;
        _logger.Log("MESSENGER\n" + _text);
    }
}