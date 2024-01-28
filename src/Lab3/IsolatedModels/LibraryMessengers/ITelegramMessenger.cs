namespace Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.LibraryMessengers;

public interface ITelegramMessenger
{
    void SendMessage(string apiKey, long userId, string message);
}