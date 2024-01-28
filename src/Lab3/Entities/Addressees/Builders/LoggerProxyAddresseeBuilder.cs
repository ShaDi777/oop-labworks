using System;
using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab3.Services.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Entities.Addressees.Builders;

public class LoggerProxyAddresseeBuilder
{
    private IAddressee? _addressee;
    private ILogger? _logger;

    public LoggerProxyAddresseeBuilder WithLogger(ILogger logger)
    {
        _logger = logger;
        return this;
    }

    public LoggerProxyAddresseeBuilder WithAddressee(IAddressee addressee)
    {
        _addressee = addressee;
        return this;
    }

    public LoggerProxyAddressee Build()
    {
        ArgumentNullException.ThrowIfNull(_addressee);
        ArgumentNullException.ThrowIfNull(_logger);

        return new LoggerProxyAddressee(_addressee, _logger);
    }
}

[SuppressMessage("category", "SA1402", Justification = "Same class")]
public class LoggerProxyAddresseeBuilder<T>
    : AddresseeAppender<T>, IAppendableAddresseeBuilder<LoggerProxyAddresseeBuilder<T>>
    where T : IAppendableAddresseeBuilder<T>
{
    private readonly LoggerProxyAddresseeBuilder _builder = new LoggerProxyAddresseeBuilder();

    public LoggerProxyAddresseeBuilder(T previousBuilder)
        : base(previousBuilder) { }

    public LoggerProxyAddresseeBuilder<T> WithLogger(ILogger logger)
    {
        _builder.WithLogger(logger);
        return this;
    }

    public LoggerProxyAddresseeBuilder<T> WithAddressee(IAddressee addressee)
    {
        _builder.WithAddressee(addressee);
        return this;
    }

    public UniversalAddresseeBuilder<LoggerProxyAddresseeBuilder<T>> ChooseAddressee()
    {
        return new UniversalAddresseeBuilder<LoggerProxyAddresseeBuilder<T>>(this);
    }

    public T AppendAddressee()
    {
        return Append(Build());
    }

    private LoggerProxyAddressee Build()
    {
        return _builder.Build();
    }
}