using System;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

[SuppressMessage("Category", "CA1305", Justification = "Parsing")]
public record Parameter(
    string FlagName,
    Parameter.StoredType Type,
    string DefaultValue = "",
    string Description = "")
{
    public enum StoredType
    {
        IntType,
        DoubleType,
        StringType,
    }

    public string Value { get; set; } = DefaultValue;

    public int GetIntValue()
    {
        return Type == StoredType.IntType ? int.Parse(Value) : throw new ArgumentException();
    }

    public double GetDoubleValue()
    {
        return Type == StoredType.DoubleType ? double.Parse(Value) : throw new ArgumentException();
    }

    public void SetValue(string value)
    {
        Value = value;
    }
}