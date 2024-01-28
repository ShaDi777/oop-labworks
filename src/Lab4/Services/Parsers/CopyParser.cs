using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;

public class CopyParser : ParserBase
{
    public CopyParser(IEnumerable<Parameter>? parameters = null)
        : base(parameters) { }

    public override ICommand? TryParse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.StartsWith("file copy", StringComparison.OrdinalIgnoreCase))
        {
            string[] args = input.Split(" ");
            ParseParameters(args);
            string sourcePath = args[2];
            string destinationPath = args[3];
            return new CopyCommand(sourcePath, destinationPath);
        }

        return ParseNext(input);
    }
}