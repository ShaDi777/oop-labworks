using System;
using Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;
using Itmo.ObjectOrientedProgramming.Lab1.Tools.Exceptions;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.HullStrengths;

public abstract class BaseHull
{
    private int _health;

    protected BaseHull(int health)
    {
        _health = health;
    }

    public bool IsFunctioning => _health > 0;

    public void ReceiveDamage(BaseObstacle obstacle)
    {
        if (obstacle is null) throw new ArgumentNullException(nameof(obstacle));

        int damage = obstacle.Damage;
        if (damage < 0) throw new NegativeValueException("Damage can't be negative");

        int mutualDamage = Math.Min(_health, damage);
        _health -= mutualDamage;
        damage -= mutualDamage;
        obstacle.Damage = damage;
    }
}
