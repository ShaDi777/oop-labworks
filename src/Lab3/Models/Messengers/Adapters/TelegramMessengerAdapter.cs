using Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.LibraryMessengers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers.Adapters;

public class TelegramMessengerAdapter : IMessenger
{
    private const string ApiKey = "KEY";
    private readonly ITelegramMessenger _telegramMessenger;

    public TelegramMessengerAdapter(ITelegramMessenger telegramMessenger)
    {
        _telegramMessenger = telegramMessenger;
    }

    public void PrintText(string text)
    {
        _telegramMessenger.SendMessage(ApiKey, 0, text);
    }
}