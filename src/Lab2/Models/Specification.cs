using System;
using System.Collections.Generic;
using System.Linq;

namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class Specification
{
    public Specification(
        string caseName = "",
        string motherBoardName = "",
        string cpuName = "",
        string gpuName = "",
        string cpuCoolingName = "",
        IEnumerable<string>? ramSticksName = null,
        IEnumerable<string>? storagesName = null,
        string wifiAdapterName = "",
        string powerSupplyUnitName = "")
    {
        CaseName = caseName;
        MotherBoardName = motherBoardName;
        CpuName = cpuName;
        GpuName = gpuName;
        CpuCoolingName = cpuCoolingName;
        RamSticksName = ramSticksName?.ToArray() ?? Array.Empty<string>();
        StoragesName = storagesName?.ToArray() ?? Array.Empty<string>();
        WifiAdapterName = wifiAdapterName;
        PowerSupplyUnitName = powerSupplyUnitName;
    }

    public string CaseName { get; set; }
    public string MotherBoardName { get; set; }
    public string CpuName { get; set; }
    public string GpuName { get; set; }
    public string CpuCoolingName { get; set; }
    public IReadOnlyCollection<string> RamSticksName { get; set; }
    public IReadOnlyCollection<string> StoragesName { get; set; }
    public string WifiAdapterName { get; set; }
    public string PowerSupplyUnitName { get; set; }
}
