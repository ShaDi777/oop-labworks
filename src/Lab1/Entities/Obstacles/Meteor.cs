using Itmo.ObjectOrientedProgramming.Lab1.Tools;

namespace Itmo.ObjectOrientedProgramming.Lab1.Entities.Obstacles;

public class Meteor : BaseObstacle
{
    public Meteor()
        : base(Settings.DamageMeteor) { }
}
