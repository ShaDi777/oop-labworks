using System;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class PcieLinesValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        int targetLinesCount = 0;
        targetLinesCount += computer.DiscreteGpu is not null ? 1 : 0;
        targetLinesCount += computer.Storages.All(storage => storage is Ssd { Connection: IStorage.ConnectionType.Pcie }) ? 1 : 0;
        if (targetLinesCount > computer.MotherBoard.PcieLineCount)
        {
            throw new NotEnoughPcieLinesException();
        }
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        int targetLinesCount = 0;
        targetLinesCount += computer.DiscreteGpu is not null ? 1 : 0;
        targetLinesCount += computer.Storages.All(storage => storage is Ssd { Connection: IStorage.ConnectionType.Pcie }) ? 1 : 0;
        return targetLinesCount > computer.MotherBoard.PcieLineCount
            ? "Not enough PCI-E lines!\n"
            : string.Empty;
    }
}
