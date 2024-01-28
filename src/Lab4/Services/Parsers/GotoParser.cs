using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;

public class GotoParser : ParserBase
{
    public GotoParser(IEnumerable<Parameter>? parameters = null)
        : base(parameters) { }

    public override ICommand? TryParse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.StartsWith("tree goto", StringComparison.OrdinalIgnoreCase))
        {
            string[] args = input.Split(" ");
            ParseParameters(args);
            string path = args[2];
            return new GotoCommand(path);
        }

        return ParseNext(input);
    }
}