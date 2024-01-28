using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Models;

public class FileResult
{
    public FileResult(bool isSuccess, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Exception = exception;
    }

    public bool IsSuccess { get; set; }
    public Exception? Exception { get; }
}