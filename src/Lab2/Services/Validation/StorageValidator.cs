using System;
using System.Linq;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public class StorageValidator : IComputerValidator
{
    public void Validate(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if (computer.Storages.Count == 0)
        {
            throw new NoStorageException();
        }

        bool isStorageInstalled =
            computer.Storages.Any(storage =>
                storage.Connection == IStorage.ConnectionType.Sata && computer.MotherBoard.SataPortsCount > 0) ||
            computer.Storages.Any(storage =>
                storage.Connection == IStorage.ConnectionType.Pcie && computer.MotherBoard.PcieLineCount > 0);

        if (!isStorageInstalled)
        {
            throw new NoStorageException();
        }
    }

    public string ValidateWithComment(Computer computer)
    {
        ArgumentNullException.ThrowIfNull(computer);

        if (computer.Storages.Select(storage =>
                storage.Connection == IStorage.ConnectionType.Sata)
                .Count() > computer.MotherBoard.SataPortsCount ||
            computer.Storages.Select(storage =>
                    storage.Connection == IStorage.ConnectionType.Pcie)
                .Count() > computer.MotherBoard.PcieLineCount)
        {
            return "Not all storages can be installed!\n";
        }

        return string.Empty;
    }
}
