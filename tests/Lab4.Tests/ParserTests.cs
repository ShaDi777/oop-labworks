using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

[SuppressMessage("Null", "SK1200", Justification = "Assertion before")]
public class ParserTests
{
    [Fact]
    public void ParserCreatesCorrectCommandConnect()
    {
        // Arrange
        const string input = @"connect C:\Windows\System32 -m local";
        ParserBase parser = new ConnectParser(new[]
        {
            new Parameter("-m", Parameter.StoredType.StringType),
        });

        // Act
        ICommand? command = parser.TryParse(input);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<ConnectCommand>(command);

        Assert.Equal(
            @"C:\Windows\System32".ToUpperInvariant(),
            (command as ConnectCommand)!.Address.ToUpperInvariant());
        Assert.Equal("LOCAL", (command as ConnectCommand)!.Mode.ToUpperInvariant());
    }

    [Fact]
    public void ParserCreatesCorrectCommandShow()
    {
        // Arrange
        const string input = @"file show C:\Windows\System32\hack.txt -m console";
        ParserBase parser = new ShowParser(new[]
        {
            new Parameter("-m", Parameter.StoredType.StringType),
        });

        // Act
        ICommand? command = parser.TryParse(input);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<ShowFileCommand>(command);

        Assert.Equal(
            @"C:\Windows\System32\hack.txt".ToUpperInvariant(),
            (command as ShowFileCommand)!.Path.ToUpperInvariant());
        Assert.Equal("CONSOLE", (command as ShowFileCommand)!.Mode.ToUpperInvariant());
    }

    [Fact]
    public void ParserCreatesCorrectCommandGoto()
    {
        // Arrange
        const string input = @"tree goto this\is\some\path";
        ParserBase parser = new GotoParser();

        // Act
        ICommand? command = parser.TryParse(input);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<GotoCommand>(command);

        Assert.Equal(
            @"this\is\some\path".ToUpperInvariant(),
            (command as GotoCommand)!.Path.ToUpperInvariant());
    }

    [Fact]
    public void ParserCreatesCorrectCommandDelete()
    {
        // Arrange
        const string input = @"file delete C:\Windows\System32\hack.txt";
        ParserBase parser = new DeleteParser();

        // Act
        ICommand? command = parser.TryParse(input);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<DeleteCommand>(command);

        Assert.Equal(
            @"C:\Windows\System32\hack.txt".ToUpperInvariant(),
            (command as DeleteCommand)!.Path.ToUpperInvariant());
    }

    [Fact]
    public void ParserCreatesCorrectCommandList()
    {
        // Arrange
        const string inputNoFlag = @"tree list";
        const string inputFlag = @"tree list -d 5";
        ParserBase parser = new ListParser(new[]
        {
            new Parameter("-d", Parameter.StoredType.IntType, "1"),
        });

        // Act
        ICommand? commandNoFlag = parser.TryParse(inputNoFlag);
        ICommand? commandFlag = parser.TryParse(inputFlag);

        // Assert
        Assert.NotNull(commandNoFlag);
        Assert.IsType<ListDirectoryCommand>(commandNoFlag);
        Assert.Equal(1, (commandNoFlag as ListDirectoryCommand)!.MaxDepth);

        Assert.NotNull(commandFlag);
        Assert.IsType<ListDirectoryCommand>(commandFlag);
        Assert.Equal(5, (commandFlag as ListDirectoryCommand)!.MaxDepth);
    }

    [Fact]
    public void ParserCreatesCorrectCommandMove()
    {
        // Arrange
        const string input = @"file move C:\Windows\System32\hack.txt C:\Windows";
        ParserBase parser = new MoveParser();

        // Act
        ICommand? command = parser.TryParse(input);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<MoveCommand>(command);

        Assert.Equal(
            @"C:\Windows\System32\hack.txt".ToUpperInvariant(),
            (command as MoveCommand)!.SourcePath.ToUpperInvariant());
        Assert.Equal(
            @"C:\Windows".ToUpperInvariant(),
            (command as MoveCommand)!.DestinationPath.ToUpperInvariant());
    }

    [Fact]
    public void ParserCreatesCorrectCommandCopy()
    {
        // Arrange
        const string input = @"file copy C:\Windows\System32\hack.txt C:\Windows";
        ParserBase parser = new CopyParser();

        // Act
        ICommand? command = parser.TryParse(input);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<CopyCommand>(command);

        Assert.Equal(
            @"C:\Windows\System32\hack.txt".ToUpperInvariant(),
            (command as CopyCommand)!.SourcePath.ToUpperInvariant());
        Assert.Equal(
            @"C:\Windows".ToUpperInvariant(),
            (command as CopyCommand)!.DestinationPath.ToUpperInvariant());
    }

    [Fact]
    public void ParserCreatesCorrectCommandRename()
    {
        // Arrange
        const string input = @"file rename C:\Windows\System32\hack.txt hack2.txt";
        ParserBase parser = new RenameParser();

        // Act
        ICommand? command = parser.TryParse(input);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<RenameCommand>(command);

        Assert.Equal(
            @"C:\Windows\System32\hack.txt".ToUpperInvariant(),
            (command as RenameCommand)!.SourcePath.ToUpperInvariant());
        Assert.Equal("hack2.txt".ToUpperInvariant(), (command as RenameCommand)!.NewFileName.ToUpperInvariant());
    }

    [SuppressMessage("Order", "SA1201", Justification = "Position before test")]
    public static IEnumerable<object[]> ChainTestData =>
        new List<object[]>
        {
            new object[] { @"connect C:\\Windows\\System32 -m local", new ConnectCommand(string.Empty, string.Empty) },
            new object[] { @"file show C:\Windows\System32\hack.txt -m console", new ShowFileCommand(string.Empty, string.Empty) },
            new object[] { @"tree goto this\is\some\path", new GotoCommand(string.Empty) },
            new object[] { @"file delete C:\Windows\System32\hack.txt", new DeleteCommand(string.Empty) },
            new object[] { @"tree list -d 5", new ListDirectoryCommand() },
            new object[] { @"file move C:\Windows\System32\hack.txt C:\Windows", new MoveCommand(string.Empty, string.Empty) },
            new object[] { @"file copy C:\Windows\System32\hack.txt C:\Windows", new CopyCommand(string.Empty, string.Empty) },
            new object[] { @"file rename C:\Windows\System32\hack.txt hack2.txt", new RenameCommand(string.Empty, string.Empty) },
        };

    [Theory]
    [MemberData(nameof(ChainTestData))]
    public void ChainOfParsersCreatesCommand(string input, ICommand expectedCommand)
    {
        ArgumentNullException.ThrowIfNull(expectedCommand);

        // Arrange
        var parser = ParserBase.MakeChain(
            new ConnectParser(new[]
            {
                new Parameter("-m", Parameter.StoredType.StringType, "local"),
            }),
            new DisconnectParser(),
            new GotoParser(),
            new ListParser(new[]
            {
                new Parameter("-d", Parameter.StoredType.IntType, "1"),
            }),
            new ShowParser(new[]
            {
                new Parameter("-m", Parameter.StoredType.StringType, "console"),
            }),
            new MoveParser(),
            new CopyParser(),
            new DeleteParser(),
            new RenameParser());

        // Act
        ICommand? command = parser.TryParse(input);

        // Assert
        Assert.NotNull(command);
        Assert.Equal(expectedCommand.GetType(), command.GetType());
    }
}