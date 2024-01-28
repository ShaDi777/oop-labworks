using System.Collections.Generic;
using System.Linq;

namespace Itmo.ObjectOrientedProgramming.Lab2.Entities;

// Optional
public sealed class Bios
{
    /*
 - Тип
 - Версия
 - Список поддерживаемых процессоров
     */
    public Bios(
        string type,
        string version,
        IEnumerable<string> supportedCpu)
    {
        Type = type;
        Version = version;
        SupportedCpu = supportedCpu.ToList();
    }

    public string Type { get; }
    public string Version { get; }
    public IEnumerable<string> SupportedCpu { get; }
}
