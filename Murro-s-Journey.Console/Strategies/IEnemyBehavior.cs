using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Strategies;

public interface IEnemyBehavior
{
    void Execute(Enemy enemy, Player player);
    string GetDescription();
}