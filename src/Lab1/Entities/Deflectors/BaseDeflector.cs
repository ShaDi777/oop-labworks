using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Tools.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Deflectors;

public abstract class BaseDeflector
{
    private int _photonDeflection;
    private int _health;

    protected BaseDeflector(int health, bool hasPhotonDeflection = false)
    {
        _health = health;
        _photonDeflection = hasPhotonDeflection ? 3 : 0;
    }

    public bool UsePhotonDeflection() { return _photonDeflection-- > 0; }

    public virtual void SetPhotonDeflection() { _photonDeflection = 3; }

    public ObstacleDamageResult EncounterObstacle(BaseObstacle obstacle)
    {
        if (obstacle is null) throw new ArgumentNullException(nameof(obstacle));

        if (obstacle is AntimatterFlare)
        {
            if (!UsePhotonDeflection()) return ObstacleDamageResult.KilledCrew;
        }

        ReceiveDamage(obstacle);
        return obstacle.IsDestroyed
                ? ObstacleDamageResult.IsDestroyed
                : ObstacleDamageResult.IsAlive;
    }

    private void ReceiveDamage(BaseObstacle obstacle)
    {
        int damage = obstacle.Damage;
        if (damage < 0) throw new NegativeValueException("Damage can't be negative");

        int mutualDamage = Math.Min(_health, damage);
        _health -= mutualDamage;
        damage -= mutualDamage;
        obstacle.Damage = damage;
    }
}
