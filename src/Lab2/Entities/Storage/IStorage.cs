namespace Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;

// Must have
public interface IStorage : IComponent
{
    /*
 - Ёмкость в Гб
 - Потребляемая мощность (в ватт)
    */
    public enum ConnectionType
    {
        Sata,
        Pcie,
    }

    public ConnectionType Connection { get; }
    public int Capacity { get; }
    public int PowerUsage { get; }
}
