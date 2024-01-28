using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.ImpulseEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Engines.JumpEngines;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.HullStrengths;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Routes;
using Itmo.ObjectOrientedProgramming.Lab1.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Ships;

public abstract class BaseShip
{
    protected BaseShip(
        BaseImpulseEngine? impulseEngine,
        BaseJumpEngine? jumpEngine,
        BaseDeflector deflector,
        bool isAntiNeutronEmitterInstalled,
        BaseHull hull,
        int weight)
    {
        ImpulseEngine = impulseEngine;
        JumpEngine = jumpEngine;
        Deflector = deflector;
        IsAntiNeutronEmitterInstalled = isAntiNeutronEmitterInstalled;
        Hull = hull;
        Weight = weight;
    }

    public BaseImpulseEngine? ImpulseEngine { get; }
    public BaseJumpEngine? JumpEngine { get; }

    public BaseDeflector Deflector { get; }

    public BaseHull Hull { get; }

    public int Weight { get; }

    public bool IsAntiNeutronEmitterInstalled { get; protected set; }

    public bool IsCrewAlive { get; protected set; } = true;

    public bool IsLost { get; protected set; }

    public ShipStatus CurrentStatus
    {
        get
        {
            if (IsLost) return ShipStatus.Lost;
            if (IsShipFunctioning) return ShipStatus.Functioning;
            return ShipStatus.Destroyed;
        }
    }

    private bool IsShipFunctioning => IsCrewAlive && Hull.IsFunctioning;

    public void InstallAntiNeutronEmitter() { IsAntiNeutronEmitterInstalled = true; }

    public void InstallPhotonDeflector() { Deflector.SetPhotonDeflection(); }

    public void EncounterObstacle(BaseObstacle obstacle)
    {
        if (obstacle is null) throw new ArgumentNullException(nameof(obstacle));

        if (obstacle is CosmoWhale && IsAntiNeutronEmitterInstalled) return;

        ObstacleDamageResult resultAfterDeflector = Deflector.EncounterObstacle(obstacle);
        switch (resultAfterDeflector)
        {
            case ObstacleDamageResult.IsDestroyed:
                break;
            case ObstacleDamageResult.IsAlive:
                Hull.ReceiveDamage(obstacle);
                break;
            case ObstacleDamageResult.KilledCrew:
                IsCrewAlive = false;
                break;
            default:
                throw new NotImplementedException("Unknown result");
        }
    }

    public BaseEngine? ChooseEngineForSegment(Segment segment)
    {
        if (segment is null) throw new ArgumentNullException(nameof(segment));

        BaseEngine? engine = segment.ChooseEngine(this);
        if (engine is null) IsLost = true;

        return engine;
    }
}
