namespace Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.LibraryMessengers;

public interface IDiscordMessenger
{
    void PostItem(long chatId, string text);
}