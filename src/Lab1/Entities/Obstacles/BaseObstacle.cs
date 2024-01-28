namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;

public abstract class BaseObstacle
{
    protected BaseObstacle(int damage)
    {
        Damage = damage;
    }

    public int Damage { get; set; }
    public bool IsDestroyed => Damage == 0;
}
