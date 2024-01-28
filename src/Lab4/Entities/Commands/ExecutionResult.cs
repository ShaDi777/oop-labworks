namespace Itmo.ObjectOrientedProgramming.Lab4.Entities.Commands;

public record ExecutionResult(bool IsSuccess, string Message, string? OutputMode = null)
{
}