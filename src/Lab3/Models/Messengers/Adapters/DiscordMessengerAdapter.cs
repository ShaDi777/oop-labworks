using Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.LibraryMessengers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers.Adapters;

public class DiscordMessengerAdapter : IMessenger
{
    private readonly IDiscordMessenger _discordMessenger;
    private readonly long _chatId;

    public DiscordMessengerAdapter(IDiscordMessenger discordMessenger, long chatId)
    {
        _discordMessenger = discordMessenger;
        _chatId = chatId;
    }

    public void PrintText(string text)
    {
        _discordMessenger.PostItem(_chatId, text);
    }
}