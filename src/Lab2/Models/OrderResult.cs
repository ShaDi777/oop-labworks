using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

[SuppressMessage("All", "CA1034", Justification = "Result types")]
public abstract record OrderResult
{
    private OrderResult() { }

    public sealed record Success() : OrderResult;

    public sealed record CompatibleWithComments(IEnumerable<string> Comments) : OrderResult;

    public sealed record WarrantyDisclaimer(string Info) : OrderResult;
}