using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class ComputerShop
{
    private readonly IEnumerable<IComputerValidator> _validators;

    public ComputerShop(IEnumerable<IComputerValidator> validators)
    {
        _validators = validators;
    }

    public OrderResult PlaceOrder(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if (computer.CpuCoolingSystem.MaxCoolingTdp < computer.Cpu.Tdp)
        {
            return new OrderResult.WarrantyDisclaimer("Cooling TDP is not enough!");
        }

        var stringBuilder = new StringBuilder();
        foreach (IComputerValidator validator in _validators)
        {
            stringBuilder.Append(validator.ValidateWithComment(computer));
        }

        var comments = stringBuilder.ToString()
            .Split("\n")
            .Where(comment => !string.IsNullOrEmpty(comment))
            .ToList();
        if (comments.Count == 0)
        {
            return new OrderResult.Success();
        }

        return new OrderResult.CompatibleWithComments(comments);
    }
}
