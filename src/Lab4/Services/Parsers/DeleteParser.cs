using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;

public class DeleteParser : ParserBase
{
    public DeleteParser(IEnumerable<Parameter>? parameters = null)
        : base(parameters) { }

    public override ICommand? TryParse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.StartsWith("file delete", StringComparison.OrdinalIgnoreCase))
        {
            string[] args = input.Split(" ");
            ParseParameters(args);
            string path = args[2];
            return new DeleteCommand(path);
        }

        return ParseNext(input);
    }
}