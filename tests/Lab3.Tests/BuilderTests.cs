using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Topics;
using Itmo.ObjectOrientedProgramming.Lab3.Models.EnumTypes;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;
using Moq;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class BuilderTests
{
    [Fact]
    public void TestLoggerWithPriorityAddressee()
    {
        var user = new UserAddressee();
        var logger = new Mock<ILogger>();
        var topicBuilder = new TopicBuilder();

        ITopic topic =
            topicBuilder.WithName("Security")
                        .ChooseAddressee()
                        .NewPriorityProxyAddressee()
                            .WithPriority(Priority.High)
                            .ChooseAddressee()
                            .NewLoggerProxyAddressee()
                                .WithLogger(logger.Object)
                                .WithAddressee(user)
                            .AppendAddressee()
                        .AppendAddressee()
                        .Build();

        Message messageLow = Message.Builder.WithTitle("Welcome")
                                        .WithText("Hello world!")
                                        .SetPriority(Priority.Low)
                                        .Build();
        Message messageHigh = Message.Builder.WithTitle("IMPORTANT")
                                        .WithText("Something important")
                                        .SetPriority(Priority.High)
                                        .Build();

        topic.SendMessage(messageLow);
        topic.SendMessage(messageHigh);

        logger.Verify(mock => mock.Log(It.IsAny<string>()), Times.Once);
        Assert.Single(user.ReceivedMessages);
    }

    [Fact]
    public void TestCompoundAddressee()
    {
        var userA = new UserAddressee();
        var userB = new UserAddressee();
        var userC = new UserAddressee();

        var message = new Mock<IMessage>();
        var topicBuilder = new TopicBuilder();

        string title = "Compound";
        ITopic topic =
            topicBuilder.WithName(title)
                .ChooseAddressee()
                .NewCompoundAddressee()
                .WithAddressee(userA)
                .WithAddressee(userB)
                .WithAddressee(userC)
                .AppendAddressee()
                .Build();

        var facade = new TopicFacade(new List<ITopic> { topic });
        facade.SendMessageByTopicName(message.Object, title);

        Assert.Single(userA.ReceivedMessages);
        Assert.Single(userB.ReceivedMessages);
        Assert.Single(userC.ReceivedMessages);
    }
}