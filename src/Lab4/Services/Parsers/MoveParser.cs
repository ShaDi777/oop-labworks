using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;

public class MoveParser : ParserBase
{
    public MoveParser(IEnumerable<Parameter>? parameters = null)
        : base(parameters) { }

    public override ICommand? TryParse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.StartsWith("file move", StringComparison.OrdinalIgnoreCase))
        {
            string[] args = input.Split(" ");
            ParseParameters(args);
            string sourcePath = args[2];
            string destinationPath = args[3];
            return new MoveCommand(sourcePath, destinationPath);
        }

        return ParseNext(input);
    }
}