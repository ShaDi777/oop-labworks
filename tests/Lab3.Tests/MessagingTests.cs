using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees;
using Itmo.ObjectOrientedProgramming.Lab3.Entities.Topics;
using Itmo.ObjectOrientedProgramming.Lab3.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.IsolatedModels.LibraryMessengers;
using Itmo.ObjectOrientedProgramming.Lab3.Models.EnumTypes;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Messengers.Adapters;
using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;
using Moq;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class MessagingTests
{
    // - При получении сообщения пользователем, оно сохраняется в статусе “не прочитано”
    [Fact]
    public void TestUserReceiveMessageAsUnread()
    {
        // Arrange
        IMessage message = Message.Builder.WithTitle("Title").WithText("Text").SetPriority(Priority.Default).Build();
        var user = new UserAddressee();
        ITopic topic = new Topic("Topic Title", user);

        // Act
        topic.SendMessage(message);

        // Assert
        Assert.Single(user.ReceivedMessages);
        Assert.True(user.ReceivedMessages.All(userMessage => !userMessage.IsRead));
    }

    // - При попытке отметить сообщение пользователя в статусе “не прочитано” как прочитанное, оно должно поменять свой статус
    [Fact]
    public void TestUserReadMessageAndStatusChange()
    {
        // Arrange
        IMessage message = Message.Builder.WithTitle("Title").WithText("Text").SetPriority(Priority.Default).Build();
        var user = new UserAddressee();
        ITopic topic = new Topic("Topic Title", user);

        // Act
        topic.SendMessage(message);
        user.ReadMessage(message.Id);

        // Assert
        Assert.True(user.ReceivedMessages.All(userMessage => userMessage.IsRead));
    }

    // - При попытке отметить сообщение пользователя в статусе “прочитано” как прочитанное, должна вернуться ошибка
    [Fact]
    public void TestUserReadMessageAndExceptionThrows()
    {
        // Arrange
        IMessage message = Message.Builder.WithTitle("Title").WithText("Text").SetPriority(Priority.Default).Build();
        var user = new UserAddressee();
        ITopic topic = new Topic("Topic Title", user);

        // Act
        topic.SendMessage(message);
        user.ReadMessage(message.Id);

        // Assert
        Assert.Throws<ReadMessageException>(() => user.ReadMessage(message.Id));
    }

    /*
    - При настроенном фильтре для адресата, отправленное сообщение, не подходящее под критерии важности -
    до адресата дойти не должно (в данном тесте необходимо использовать моки)
    */
    [Fact]
    public void TestPriorityProxyBlockMessage()
    {
        // Arrange
        var message = new Mock<IMessage>();
        message.SetupGet(x => x.Priority).Returns(Priority.Low);

        var hiddenUser = new Mock<IAddressee>();
        var priorityProxy = new PriorityProxyAddressee(hiddenUser.Object, Priority.High);

        var topic = new Topic("Title", priorityProxy);

        // Act
        topic.SendMessage(message.Object);

        // Assert
        hiddenUser.Verify(mock => mock.ReceiveMessage(message.Object), Times.Never);
    }

    /*
    - При настроенном логгировании адресата, должен писаться лог, когда приходит сообщение
    (в данном тесте необходимо использовать моки)
    */
    [Fact]
    public void TestLoggerProxyDoLog()
    {
        // Arrange
        var message = new Mock<IMessage>();
        var addressee = new Mock<IAddressee>();
        var logger = new Mock<ILogger>();
        var loggerProxy = new LoggerProxyAddressee(addressee.Object, logger.Object);

        var topic = new Topic("Title", loggerProxy);

        // Act
        topic.SendMessage(message.Object);

        // Assert
        logger.Verify(mock => mock.Log(It.IsAny<string>()), Times.Once);
    }

    /*
    - При отправке сообщения в месенджер, его реализация должна производить ожидаемое значение
    (в данном тесте необходимо использовать моки)
     */
    [Fact]
    public void TestMessenger()
    {
        // Arrange
        var message = new Mock<IMessage>();
        var logger = new Mock<ILogger>();

        var messenger = new Messenger(logger.Object);
        var messengerAddressee = new MessengerAddressee(messenger);

        var topic = new Topic("Title", messengerAddressee);

        // Act
        topic.SendMessage(message.Object);

        // Assert
        logger.Verify(mock => mock.Log(It.IsRegex("^MESSENGER")), Times.Once);
    }

    [Fact]
    public void TestExternalMessengers()
    {
        // Arrange
        var message = new Mock<IMessage>();
        var logger = new Mock<ILogger>();

        var telegram = new Telegram(logger.Object);
        var discord = new Discord(logger.Object);
        var telegramAddressee = new MessengerAddressee(new TelegramMessengerAdapter(telegram));
        var discordAddressee = new MessengerAddressee(new DiscordMessengerAdapter(discord, 0));

        var topicTelegram = new Topic("Title", telegramAddressee);
        var topicDiscord = new Topic("Title", discordAddressee);

        // Act
        topicTelegram.SendMessage(message.Object);
        topicDiscord.SendMessage(message.Object);

        // Assert
        logger.Verify(mock => mock.Log(It.IsRegex("^Telegram")), Times.Once);
        logger.Verify(mock => mock.Log(It.IsRegex("^Discord")), Times.Once);
    }

    [Fact]
    public void TestDisplay()
    {
        // Arrange
        var message = new Mock<IMessage>();
        var logger = new Mock<ILogger>();
        var displayDriver = new Mock<IDisplayDriver>();

        var display = new Display(logger.Object, displayDriver.Object);
        var messengerAddressee = new DisplayAddressee(display);

        var topic = new Topic("Title", messengerAddressee);

        // Act
        topic.SendMessage(message.Object);

        // Assert
        logger.Verify(mock => mock.Log(It.IsAny<string>()), Times.Once);
    }
}