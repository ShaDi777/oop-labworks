using System;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;
using Itmo.ObjectOrientedProgramming.Lab4.Models.Contexts;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Printers;
using Itmo.ObjectOrientedProgramming.Lab4.Services.Traversal;
using Itmo.ObjectOrientedProgramming.Lab4.Services.TreeOutputCreator;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public static class Entrypoint
{
    public static void Main()
    {
        var treeTraversal = new TreeTraversalNoHidden(new TreeTraversal());
        var treeStringCreator = new TreeStringCreator();
        IContext context = new Context(null, null, treeTraversal, treeStringCreator);

        var parser = ParserBase.MakeChain(
            new ConnectParser(new[]
                {
                    new Parameter("-m", Parameter.StoredType.StringType, "local", Description: "режим файловой системы"),
                }),
            new DisconnectParser(),
            new GotoParser(),
            new ListParser(new[]
            {
                new Parameter("-d", Parameter.StoredType.IntType, "1", Description: "глубина выборки"),
            }),
            new ShowParser(new[]
            {
                new Parameter("-m", Parameter.StoredType.StringType, "console", Description: "режим вывода файла"),
            }),
            new MoveParser(),
            new CopyParser(),
            new DeleteParser(),
            new RenameParser());

        var consolePrinter = new ConsolePrinter();
        var filePrinter = new FilePrinter("FileOutput.txt");
        IPrinter printer = consolePrinter;

        while (true)
        {
            printer.Print(context.FullPath + ">");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            ICommand? command = parser.TryParse(input);
            ExecutionResult result = command?.Execute(context) ??
                                     new ExecutionResult(false, "Couldn't parse command");
            if (result.OutputMode is null)
            {
                printer.PrintLine(result.Message);
            }
            else
            {
                switch (result.OutputMode)
                {
                    case "CONSOLE":
                        consolePrinter.PrintLine(result.Message);
                        break;
                    case "FILE":
                        filePrinter.PrintLine(result.Message);
                        break;
                }
            }

            if (command is DisconnectCommand) break;
        }

        filePrinter.Dispose();
    }
}