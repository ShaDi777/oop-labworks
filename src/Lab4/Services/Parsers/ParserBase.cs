using System;
using System.Collections.Generic;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Services.Parsers;

public abstract class ParserBase
{
    private ParserBase? _next;

    protected ParserBase(IEnumerable<Parameter>? parameters = null)
    {
        Parameters = parameters ?? Array.Empty<Parameter>();
    }

    protected IEnumerable<Parameter> Parameters { get; }

    public static ParserBase MakeChain(ParserBase first, params ParserBase[] chain)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(chain);

        ParserBase head = first;
        foreach (ParserBase nextInChain in chain)
        {
            head._next = nextInChain;
            head = nextInChain;
        }

        return first;
    }

    public abstract ICommand? TryParse(string input);

    protected ICommand? ParseNext(string input)
    {
        return _next?.TryParse(input);
    }

    protected void ParseParameters(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        for (int i = 0; i < args.Length; i++)
        {
            Parameter? param = Parameters.FirstOrDefault(param => param.FlagName == args[i]);
            if (param is not null)
            {
                param.Value = args[++i];
            }
        }
    }
}