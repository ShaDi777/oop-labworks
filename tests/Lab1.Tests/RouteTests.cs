using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Routes;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Services;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

[SuppressMessage("Test", "CA1707", Justification = "Test method naming convention")]
public class RouteTests
{
    public static IEnumerable<object[]> Data1 =>
        new List<object[]>
        {
            new object[] { new ShuttleShip(), ShipStatus.Lost },
            new object[] { new AvgurShip(),  ShipStatus.Lost },
        };

    public static IEnumerable<object[]> Data2 =>
        new List<object[]>
        {
            new object[] { false, false, },
            new object[] { true, true, },
        };

    public static IEnumerable<object[]> Data3 =>
        new List<object[]>
        {
            new object[] { new VaklasShip(), ShipStatus.Destroyed },
            new object[] { new AvgurShip(), ShipStatus.Functioning },
            new object[] { new MeridianShip(), ShipStatus.Functioning },
        };

    public SimpleSimulation Simulation { get; } = new SimpleSimulation();

    /*
     * Test 1
     * Маршрут средней длины в туманности повышенной плотности пространства.
     * Обработать два корабля ([Theory]): Прогулочный челнок и Авгур.
     * Первый не имеет прыжковых двигателей, второй имеет недостаточную дальность.
     * Оба не должны завершить маршрут.
     */
    [Theory]
    [MemberData(nameof(Data1))]
    public void SimulateSingleShip_MediumDistanceJump_ShouldNotFinish(
        BaseShip ship, ShipStatus expectedResult)
    {
        // Arrange
        var obstacles1 = new List<BaseObstacle>();
        var environment1 = new HighDensitySpace(obstacles1);
        var segment1 = new Segment(environment1, 5000);
        var route = new Route(new List<Segment> { segment1 });

        // Act
        Statistics statistics = Simulation.SimulateCase(ship, route);

        // Assert
        Assert.Equal(expectedResult, statistics.CurrentShipStatus);
    }

    /*
     * Test 2
     * Вспышка антиматерии в подпространственном канале.
     * Обработать два корабля ([Theory]):  Ваклас и Ваклас с фотонными дефлекторами.
     * В первом случае маршрут не должен быть пройден из-за потери экипажа,
     * во втором – пройден.
     */
    [Theory]
    [MemberData(nameof(Data2))]
    public void SimulateSingleShip_HighDensitySpaceWithAntimatterFlare_ShouldReturnResult(
        bool hasPhotonDeflector, bool expectedResult)
    {
        // Arrange
        var ship = new VaklasShip();
        if (hasPhotonDeflector) ship.InstallPhotonDeflector();

        var obstacles1 = new List<BaseObstacle> { new AntimatterFlare(), };
        var environment1 = new HighDensitySpace(obstacles1);
        var segment1 = new Segment(environment1, 1000);
        var route = new Route(new List<Segment> { segment1 });

        // Act
        Statistics statistics = Simulation.SimulateCase(ship, route);

        // Assert
        Assert.Equal(expectedResult, statistics.IsCrewAlive);
    }

    /*
     * Test 3
     * Космо-кит в туманности нитринных частиц.
     * Обработать три корабля ([Theory]): Ваклас, Авгур и Мередиан.
     * Первый – уничтожен после столкновения,
     * второй – только потерял щиты,
     * третий – был не тронут.
     */
    [Theory]
    [MemberData(nameof(Data3))]
    public void SimulateSingleShip_NeutronParticlesSpaceWithCosmoWhale_ShouldReturnResult(
        BaseShip ship, ShipStatus expectedResult)
    {
        // Arrange
        var obstacles1 = new List<BaseObstacle> { new CosmoWhale(), };
        var environment1 = new NeutronParticlesSpace(obstacles1);
        var segment1 = new Segment(environment1, 1000);
        var route = new Route(new List<Segment> { segment1 });

        // Act
        Statistics statistics = Simulation.SimulateCase(ship, route);

        // Assert
        Assert.Equal(expectedResult, statistics.CurrentShipStatus);
    }

    /*
     * Test 4
     * Короткий маршрут в обычном космосе.
     * Запускаем Прогулочный челнок и Ваклас.
     * Т.к. у Вакласа большая стоимость полёта,
     * то Прогулочный челнок должен быть оптимальнее для данного маршрута.
     */
    [Fact]
    public void SimulateRoute_NormalSpace_ShouldChooseOptimal()
    {
        // Arrange
        var shuttle = new ShuttleShip();
        var vaklas = new VaklasShip();

        var ships = new List<BaseShip> { shuttle, vaklas };

        var obstacles = new List<BaseObstacle>();
        var environment = new NormalSpace(obstacles);
        var segment = new Segment(environment, 1000);

        var route = new Route(new[] { segment });

        // Act
        BaseShip? optimalShip = Simulation.FindOptimalShip(ships, route);

        // Assert
        Assert.NotNull(optimalShip);
        Assert.Equal(shuttle, optimalShip);
    }

    /*
     * Test 5
     * Маршрут средней длины в туманности повышенной плотности пространства.
     * Запускаем Авгур и Стеллу.
     * Т.к. у Авгура возможная дальность прохождения по подпространственным
     * каналам меньше – должна быть выбрана Стелла.
     */
    [Fact]
    public void SimulateRoute_HighDensitySpace_ShouldChooseOptimal()
    {
        // Arrange
        var avgur = new AvgurShip();
        var stella = new StellaShip();

        var ships = new List<BaseShip> { avgur, stella };

        var obstacles = new List<BaseObstacle>();
        var environment = new HighDensitySpace(obstacles);
        var segment = new Segment(environment, 5000);

        var route = new Route(new[] { segment });

        // Act
        BaseShip? optimalShip = Simulation.FindOptimalShip(ships, route);

        // Assert
        Assert.NotNull(optimalShip);
        Assert.Equal(stella, optimalShip);
    }

    /*
     * Test 6
     * Маршрут в туманости нитринных частиц.
     * Запускаем Прогулочный челнок и Ваклас.
     * Должен быть выбран Ваклас.
     */
    [Fact]
    public void SimulateRoute_NeutronParticlesSpace_ShouldChooseOptimal()
    {
        // Arrange
        var shuttle = new ShuttleShip();
        var vaklas = new VaklasShip();

        var ships = new List<BaseShip> { shuttle, vaklas };

        var obstacles = new List<BaseObstacle>();
        var environment = new NeutronParticlesSpace(obstacles);
        var segment = new Segment(environment, 10000);

        var route = new Route(new[] { segment });

        // Act
        BaseShip? optimalShip = Simulation.FindOptimalShip(ships, route);

        // Assert
        Assert.NotNull(optimalShip);
        Assert.Equal(vaklas, optimalShip);
    }

    /*
     * Test 7
     * Маршрут из нескольких отрезков пути с препятсвиями и без.
     * Детали маршрута реализуются по усмотрению студента.
     */
    [Fact]
    public void SimulateRoute_DifferentEnvironments_ShouldChooseOptimal()
    {
        // Arrange
        var shuttle = new ShuttleShip(); // ImpulseEngineC -> don't finish
        var vaklas = new VaklasShip(); // Finish
        var meridian = new MeridianShip(); // No jump engine -> don't finish
        var stella = new StellaShip(); // ImpulseEngineC -> don't finish
        var avgur = new AvgurShip(); // Finish

        var ships = new List<BaseShip> { shuttle, vaklas, meridian, stella, avgur };

        var obstacles1 = new List<BaseObstacle> { new Asteroid(), new Meteor() };
        var environment1 = new NormalSpace(obstacles1);
        var segment1 = new Segment(environment1, 1000);

        var obstacles2 = new List<BaseObstacle>();
        var environment2 = new HighDensitySpace(obstacles2);
        var segment2 = new Segment(environment2, 1000);

        var obstacles3 = new List<BaseObstacle> { new CosmoWhale() };
        var environment3 = new NeutronParticlesSpace(obstacles3);
        var segment3 = new Segment(environment3, 1000);

        var route = new Route(new[] { segment1, segment2, segment3 });

        // Act
        BaseShip? optimalShip = Simulation.FindOptimalShip(ships, route);

        // Assert
        Assert.NotNull(optimalShip);
        Assert.Equal(avgur, optimalShip);
    }
}