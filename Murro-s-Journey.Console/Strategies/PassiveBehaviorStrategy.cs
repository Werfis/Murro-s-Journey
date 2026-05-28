using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Strategies;

public class PassiveBehaviorStrategy : IEnemyBehavior
{
    public void Execute(Enemy enemy, Player player)
    {
        System.Console.WriteLine($"{enemy.Name} stands still, doing nothing...");
    }

    public string GetDescription()
    {
        return "Passive strategy (does nothing)";
    }
}