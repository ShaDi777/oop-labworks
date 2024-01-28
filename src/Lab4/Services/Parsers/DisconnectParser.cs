using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;

public class DisconnectParser : ParserBase
{
    public DisconnectParser(IEnumerable<Parameter>? parameters = null)
        : base(parameters) { }

    public override ICommand? TryParse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        string[] args = input.Split(" ");
        ParseParameters(args);
        return input.Equals("disconnect", StringComparison.OrdinalIgnoreCase)
            ? new DisconnectCommand()
            : ParseNext(input);
    }
}