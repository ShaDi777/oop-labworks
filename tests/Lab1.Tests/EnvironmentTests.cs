using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Environments;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Tools.Exceptions;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

[SuppressMessage("Test", "CA1707", Justification = "Test method naming convention")]
public class EnvironmentTests
{
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[]
            {
                new List<BaseObstacle>
                {
                    new Asteroid(),
                    new Meteor(),
                    new CosmoWhale(),
                },
            },
            new object[]
            {
                new List<BaseObstacle>
                {
                    new AntimatterFlare(),
                    new Asteroid(),
                    new Meteor(),
                },
            },
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void ConstructorNormalSpace_IllegalObstacle_ShouldThrowException(
         ICollection<BaseObstacle> obstacles)
    {
        // Arrange and Act
        Func<NormalSpace> action = () => new NormalSpace(obstacles);

        // Assert
        Assert.Throws<IllegalObstacleException>(action);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void ConstructorHighDensitySpace_IllegalObstacle_ShouldThrowException(
        ICollection<BaseObstacle> obstacles)
    {
        // Arrange and Act
        Func<HighDensitySpace> action = () => new HighDensitySpace(obstacles);

        // Assert
        Assert.Throws<IllegalObstacleException>(action);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void ConstructorNeutronParticlesSpace_IllegalObstacle_ShouldThrowException(
        ICollection<BaseObstacle> obstacles)
    {
        // Arrange and Act
        Func<NeutronParticlesSpace> action = () => new NeutronParticlesSpace(obstacles);

        // Assert
        Assert.Throws<IllegalObstacleException>(action);
    }

    [Fact]
    public void ConstructorNormalSpace_ShouldCreateObject()
    {
        // Arrange
        var obstacles = new List<BaseObstacle>
        {
            new Asteroid(),
            new Meteor(),
        };

        // Act
        var environment = new NormalSpace(obstacles);

        // Assert
        Assert.NotNull(environment);
    }

    [Fact]
    public void ConstructorHighDensitySpace_ShouldCreateObject()
    {
        // Arrange
        var obstacles = new List<BaseObstacle>
        {
            new AntimatterFlare(),
        };

        // Act
        var environment = new HighDensitySpace(obstacles);

        // Assert
        Assert.NotNull(environment);
    }

    [Fact]
    public void ConstructorNeutronParticlesSpace_ShouldCreateObject()
    {
        // Arrange
        var obstacles = new List<BaseObstacle>
        {
            new CosmoWhale(),
        };

        // Act
        var environment = new NeutronParticlesSpace(obstacles);

        // Assert
        Assert.NotNull(environment);
    }
}