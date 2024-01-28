using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;

public class Asteroid : BaseObstacle
{
    public Asteroid()
        : base(Settings.DamageAsteroid) { }
}
