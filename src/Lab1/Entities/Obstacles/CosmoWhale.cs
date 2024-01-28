using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;

public class CosmoWhale : BaseObstacle
{
    public CosmoWhale(int density = 1)
        : base(Settings.DamageCosmoWhale * density)
    {
        Density = density;
    }

    public int Density { get; private set; }
}
