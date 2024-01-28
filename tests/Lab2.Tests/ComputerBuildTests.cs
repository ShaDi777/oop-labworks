using System;
using System.Collections.Generic;
using Itmo.ObjectOrientedProgramming.Lab2.Entities;
using Itmo.ObjectOrientedProgramming.Lab2.Entities.Storage;
using Itmo.ObjectOrientedProgramming.Lab2.Exceptions;
using Itmo.ObjectOrientedProgramming.Lab2.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Models.EnumTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Services;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Repositories.ComponentRepositories;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Validation;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class ComputerBuildTests
{
    private IPartsRepository PartsFactory { get; } = new PartsRepository(
        new CpuRepository(new Dictionary<string, Cpu>()
        {
            {
                "Intel i5 12400F",
                new Cpu(
                    name: "Intel i5 12400F",
                    clockRate: 2.5,
                    cores: 6,
                    socket: new SocketType("LGA1700"),
                    hasIntegratedGraphics: false,
                    supportedRamType: new List<RamVersionFrequency>() { new(5, 4800), new(4, 3200) },
                    tdp: 117,
                    powerConsumption: 117)
            },
            {
                "Intel i5 10400F",
                new Cpu(
                    name: "Intel i5 10400F",
                    clockRate: 2.9,
                    cores: 6,
                    socket: new SocketType("LGA1200"),
                    hasIntegratedGraphics: false,
                    supportedRamType: new List<RamVersionFrequency>() { new(4, 2666) },
                    tdp: 65,
                    powerConsumption: 65)
            },
            {
                "AMD Ryzen 5 5600G",
                new Cpu(
                    name: "AMD Ryzen 5 5600G",
                    clockRate: 3.9,
                    cores: 6,
                    socket: new SocketType("AM4"),
                    hasIntegratedGraphics: true,
                    supportedRamType: new List<RamVersionFrequency>() { new(4, 3200) },
                    tdp: 65,
                    powerConsumption: 65)
            },
            {
                "AMD Ryzen 7 5700X",
                new Cpu(
                    name: "AMD Ryzen 7 5700X",
                    clockRate: 3.4,
                    cores: 8,
                    socket: new SocketType("AM4"),
                    hasIntegratedGraphics: false,
                    supportedRamType: new List<RamVersionFrequency>() { new(4, 3200) },
                    tdp: 65,
                    powerConsumption: 65)
            },
        }),
        new GpuRepository(new Dictionary<string, DiscreteGpu>()
        {
            {
                "GeForce RTX 3050",
                new DiscreteGpu(
                    "GeForce RTX 3050",
                    new DimensionInfo(172, 120, 47),
                    8,
                    4,
                    1552,
                    115)
            },
            {
                "GeForce RTX 3060 Ti",
                new DiscreteGpu(
                    "GeForce RTX 3060 Ti",
                    new DimensionInfo(285, 124, 58),
                    8,
                    4,
                    1530,
                    225)
            },
            {
                "GeForce GTX 1660 SUPER",
                new DiscreteGpu(
                    "GeForce GTX 1660 SUPER",
                    new DimensionInfo(214, 119, 39),
                    6,
                    3,
                    1552,
                    125)
            },
            {
                "GeForce RTX 4060",
                new DiscreteGpu(
                    "GeForce RTX 4060",
                    new DimensionInfo(250, 124, 40),
                    8,
                    4,
                    1830,
                    130)
            },
        }),
        new CaseRepository(new Dictionary<string, ComputerCase>()
        {
            {
                "DEEPCOOL MATREXX 30",
                new ComputerCase(
                    "DEEPCOOL MATREXX 30",
                    new DimensionInfo(250, 185, 151),
                    new List<MotherboardFormFactorType>()
                        { MotherboardFormFactorType.MicroATX, MotherboardFormFactorType.MiniATX },
                    new DimensionInfo(406, 193, 378))
            },
            {
                "MONTECH AIR 100",
                new ComputerCase(
                    "MONTECH AIR 100",
                    new DimensionInfo(330, 200, 161),
                    new List<MotherboardFormFactorType>()
                        { MotherboardFormFactorType.MicroATX, MotherboardFormFactorType.MiniATX },
                    new DimensionInfo(405, 210, 425))
            },
            {
                "DEEPCOOL CC560",
                new ComputerCase(
                    "DEEPCOOL CC560",
                    new DimensionInfo(370, 200, 163),
                    new List<MotherboardFormFactorType>()
                    {
                        MotherboardFormFactorType.StandardATX,
                        MotherboardFormFactorType.MicroATX,
                        MotherboardFormFactorType.MiniATX,
                    },
                    new DimensionInfo(416, 210, 477))
            },
        }),
        new CpuCoolingSystemRepository(new Dictionary<string, CpuCoolingSystem>()
        {
            {
                "Weak Cooler",
                new CpuCoolingSystem(
                    "Weak Cooler",
                    new DimensionInfo(92, 125, 150),
                    new List<SocketType>()
                    {
                        new("AM4"),
                        new("AM5"),
                        new("LGA1700"),
                        new("LGA1200"),
                        new("LGA1155"),
                        new("LGA1151"),
                        new("LGA1150"),
                    },
                    20)
            },
            {
                "Normal Cooler",
                new CpuCoolingSystem(
                    "Normal Cooler",
                    new DimensionInfo(92, 125, 150),
                    new List<SocketType>()
                    {
                        new("AM4"),
                        new("AM5"),
                        new("LGA1700"),
                        new("LGA1200"),
                        new("LGA1155"),
                        new("LGA1151"),
                        new("LGA1150"),
                    },
                    100)
            },
            {
                "Strong Cooler",
                new CpuCoolingSystem(
                    "Strong Cooler",
                    new DimensionInfo(92, 125, 150),
                    new List<SocketType>()
                    {
                        new("AM4"),
                        new("AM5"),
                        new("LGA1700"),
                        new("LGA1200"),
                        new("LGA1155"),
                        new("LGA1151"),
                        new("LGA1150"),
                    },
                    220)
            },
        }),
        new RamStickRepository(new Dictionary<string, RamStick>()
        {
            {
                "8 gb",
                new RamStick(
                    "8 gb",
                    8,
                    new List<JedecProfile>()
                    {
                        new(3200, 1.35),
                    },
                    new List<XmpProfile>()
                    {
                        new XmpProfile(new int[] { 14, 16, 16, 32 }, 1.35, 3000),
                        new XmpProfile(new int[] { 16, 18, 18, 36 }, 1.35, 3200),
                    },
                    RamFormFactorType.DIMM,
                    4,
                    6)
            },
            {
                "16 gb",
                new RamStick(
                    "16 gb",
                    16,
                    new List<JedecProfile>()
                    {
                        new(3600, 1.35),
                    },
                    new List<XmpProfile>()
                    {
                        new XmpProfile(new int[] { 18, 22, 22, 44 }, 1.35, 3600),
                    },
                    RamFormFactorType.DIMM,
                    4,
                    7)
            },
        }),
        new WifiAdapterRepository(new Dictionary<string, WifiAdapter>()
        {
            { "Wifi adapter", new WifiAdapter("Wifi adapter", 6, true, 3, 10) },
        }),
        new StorageRepository(new Dictionary<string, IStorage>()
        {
            { "hdd", new Hdd("hdd", 6000, 1024, 2) },
            { "ssd", new Ssd("ssd", IStorage.ConnectionType.Pcie, 3000, 1024, 1) },
        }),
        new MotherBoardRepository(new Dictionary<string, MotherBoard>()
        {
            {
                "B550",
                new MotherBoard(
                    "B550",
                    new SocketType("AM4"),
                    2,
                    4,
                    new ChipsetInfo(
                        false,
                        true,
                        new int[] { 3400, 3466, 3600, 3733, 3866, 4000, 4133, 4266, 4400, 4600, 4733 }),
                    4,
                    4,
                    MotherboardFormFactorType.MicroATX,
                    new Bios(
                        "simpleAmd",
                        "1.0.0",
                        new[] { "AMD Ryzen 5 5600G" }))
            },
            {
                "B550v2",
                new MotherBoard(
                    "B550v2",
                    new SocketType("AM4"),
                    4,
                    4,
                    new ChipsetInfo(
                        true,
                        true,
                        new int[] { 3200, 3400, 3466, 3600, 3733, 3866, 4000, 4133, 4266, 4400, 4600, 4733 }),
                    4,
                    4,
                    MotherboardFormFactorType.StandardATX,
                    new Bios(
                        "simpleAmd",
                        "1.0.0",
                        new[] { "AMD Ryzen 7 5700X" }))
            },
            {
                "B760M",
                new MotherBoard(
                    "B760M",
                    new SocketType("LGA1700"),
                    2,
                    4,
                    new ChipsetInfo(
                        true,
                        true,
                        new int[] { 3200, 4000, 4266, 4400, 5333 }),
                    4,
                    4,
                    MotherboardFormFactorType.MicroATX,
                    new Bios(
                        "simpleIntel",
                        "1.0.0",
                        new[] { "Intel i5 12400F" }))
            },
            {
                "H470",
                new MotherBoard(
                    "H470",
                    new SocketType("LGA1200"),
                    1,
                    4,
                    new ChipsetInfo(
                        false,
                        false,
                        new int[] { 2933 }),
                    4,
                    4,
                    MotherboardFormFactorType.MicroATX,
                    new Bios(
                        "verySimpleIntel",
                        "1.0.0",
                        new[] { "Intel i5 10400F" }))
            },
        }),
        new PowerSupplyUnitRepository(new Dictionary<string, PowerSupplyUnit>()
        {
            { "Weak", new PowerSupplyUnit("Weak", 150) },
            { "Normal",  new PowerSupplyUnit("Normal", 500) },
            { "Strong", new PowerSupplyUnit("Strong", 1000) },
        }));

    private IEnumerable<IComputerValidator> Validators { get; } = new List<IComputerValidator>
    {
        new CaseValidator(),
        new CoolingValidator(),
        new GpuValidator(),
        new NetworkValidator(),
        new StorageValidator(),
        new PcieLinesValidator(),
        new PowerSupplyValidator(),
        new MotherboardAndCpuValidator(),
        new MotherboardCpuAndRamValidator(),
    };

    [Fact]
    public void TestSuccessComputer()
    {
        // Arrange
        Computer computer = Computer.SequentialBuilder(PartsFactory, Validators)
            .WithCase("DEEPCOOL CC560")
            .WithPowerSupplyUnit("Strong")
            .WithMotherBoard("B550v2")
            .WithCpu("AMD Ryzen 7 5700X")
            .WithCpuCoolingSystem("Strong cooler")
            .WithGpu("GeForce Rtx 4060")
            .AddRam("8 gb").AddRam("8 gb").AddRam("8 gb").AddRam("8 gb")
            .AddStorage("SSD").Build();
        var shop = new ComputerShop(Validators);

        // Act
        OrderResult orderResult = shop.PlaceOrder(computer);

        // Assert
        Assert.IsType<OrderResult.Success>(orderResult);
    }

    [Fact]
    public void TestPowerlessComputer()
    {
        // Arrange
        Computer computer = Computer.SequentialBuilder(PartsFactory, Validators)
            .WithCase("DEEPCOOL CC560")
            .WithPowerSupplyUnit("Weak")
            .WithMotherBoard("B760M")
            .WithCpu("Intel i5 12400F")
            .WithCpuCoolingSystem("Strong cooler")
            .WithGpu("GeForce Rtx 3060 Ti")
            .AddRam("8 gb").AddRam("8 gb").AddRam("8 gb").AddRam("8 gb")
            .AddStorage("SSD").Build();
        var shop = new ComputerShop(Validators);

        // Act
        OrderResult orderResult = shop.PlaceOrder(computer);

        // Assert
        Assert.IsType<OrderResult.CompatibleWithComments>(orderResult);
        var orderResultCompat = orderResult as OrderResult.CompatibleWithComments;
        Assert.NotNull(orderResultCompat);
        Assert.Contains(
            orderResultCompat.Comments,
            comment => comment.StartsWith("Not enough power!", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void TestWeakCoolerComputer()
    {
        // Arrange
        Computer computer = Computer.SequentialBuilder(PartsFactory, Validators)
            .WithCase("DEEPCOOL CC560")
            .WithPowerSupplyUnit("Normal")
            .WithMotherBoard("B760M")
            .WithCpu("Intel i5 12400F")
            .WithCpuCoolingSystem("Weak cooler")
            .WithGpu("GeForce Rtx 3060 Ti")
            .AddRam("8 gb").AddRam("8 gb").AddRam("8 gb").AddRam("8 gb")
            .AddStorage("SSD").Build();
        var shop = new ComputerShop(Validators);

        // Act
        OrderResult orderResult = shop.PlaceOrder(computer);

        // Assert
        Assert.IsType<OrderResult.WarrantyDisclaimer>(orderResult);
        if (orderResult is OrderResult.WarrantyDisclaimer orderResultCompat)
            Assert.Equal("Cooling TDP is not enough!", orderResultCompat.Info);
    }

    [Fact]
    public void TestSocketIncompatibleComputer()
    {
        Assert.Throws<SocketIncompatibilityException>(
            () => Computer.SequentialBuilder(PartsFactory, Validators)
                .WithCase("DEEPCOOL CC560")
                .WithPowerSupplyUnit("Strong")
                .WithMotherBoard("B760M")
                .WithCpu("AMD Ryzen 5 5600G")
                .WithCpuCoolingSystem("Strong cooler")
                .WithGpu("GeForce Rtx 3060 Ti")
                .AddRam("8 gb").AddRam("8 gb").AddRam("8 gb").AddRam("8 gb")
                .AddStorage("SSD").Build());
    }

    [Fact]
    public void TestNoGpuComputer()
    {
        Assert.Throws<NoGpuException>(
            () => Computer.SequentialBuilder(PartsFactory, Validators)
                .WithCase("DEEPCOOL CC560")
                .WithPowerSupplyUnit("Strong")
                .WithMotherBoard("B550v2")
                .WithCpu("AMD Ryzen 7 5700X")
                .WithCpuCoolingSystem("Strong cooler")
                .AddRam("8 gb").AddRam("8 gb")
                .AddStorage("SSD").Build());
    }

    [Fact]
    public void TestFormFactorExceptionComputer()
    {
        Assert.Throws<FormFactorIncompatibilityException>(
            () => Computer.SequentialBuilder(PartsFactory, Validators)
                .WithCase("MONTECH AIR 100")
                .WithPowerSupplyUnit("Strong")
                .WithMotherBoard("B550v2")
                .WithCpu("AMD Ryzen 7 5700X")
                .WithCpuCoolingSystem("Strong cooler")
                .WithGpu("GeForce RTX 4060")
                .AddRam("8 gb").AddRam("8 gb")
                .AddStorage("SSD").Build());
    }
}
