using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.Displays;

public class Display : IDisplay
{
    private readonly ILogger _logger;
    private readonly IDisplayDriver _driver;

    public Display(ILogger logger, IDisplayDriver driver)
    {
        _logger = logger;
        _driver = driver;
    }

    public void PrintColoredText(string text)
    {
        _driver.ClearOutput();
        _driver.SetText(text);
        _logger.Log(_driver.GetColoredText());
    }
}