using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;

public class ConnectParser : ParserBase
{
    public ConnectParser(IEnumerable<Parameter>? parameters = null)
        : base(parameters) { }

    public override ICommand? TryParse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (input.StartsWith("connect", StringComparison.OrdinalIgnoreCase))
        {
            string[] args = input.Split(" ");
            ParseParameters(args);
            return new ConnectCommand(args[1], Parameters.First(param => param.FlagName == "-m").Value);
        }

        return ParseNext(input);
    }
}