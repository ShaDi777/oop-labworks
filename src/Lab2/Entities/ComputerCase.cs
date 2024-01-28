using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EnumTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Must have
public sealed class ComputerCase : IComponent
{
    /*
 - Максимальная длина и ширина видеокарты
 - Поддерживаемые форм-факторы материнских плат
 - Габариты
     */
    public ComputerCase(
        string name,
        DimensionInfo gpuMaxDimensions,
        IEnumerable<MotherboardFormFactorType> supportedFormFactors,
        DimensionInfo dimensions)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} can't be empty");

        Name = name;
        GpuMaxDimensions = gpuMaxDimensions;
        SupportedFormFactors = supportedFormFactors;
        Dimensions = dimensions;
    }

    public string Name { get; }
    public DimensionInfo GpuMaxDimensions { get; }
    public IEnumerable<MotherboardFormFactorType> SupportedFormFactors { get; }
    public DimensionInfo Dimensions { get; }
}
