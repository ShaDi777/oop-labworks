using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Topics;

public class TopicFacade
{
    private readonly IList<ITopic> _topics;

    public TopicFacade(IList<ITopic> topics)
    {
        _topics = topics;
    }

    public void AddTopic(ITopic topic)
    {
        _topics.Add(topic);
    }

    public void SendMessageToAddressee(IMessage message, IAddressee addressee)
    {
        _topics.First(topic => topic.Addressee == addressee).SendMessage(message);
    }

    public void SendMessageByTopicName(IMessage message, string name)
    {
        _topics.First(topic => topic.Name == name).SendMessage(message);
    }
}