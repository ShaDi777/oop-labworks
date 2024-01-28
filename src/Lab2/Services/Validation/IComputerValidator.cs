using Itmo.ObjectOrientedProgramming.Lab2.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;

public interface IComputerValidator
{
    void Validate(Computer computer);
    string ValidateWithComment(Computer computer);
}
