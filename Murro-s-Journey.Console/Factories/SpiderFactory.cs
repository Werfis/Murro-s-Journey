using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Factories;

public class SpiderFactory : EnemyFactory
{
    public override Enemy CreateEnemy(int startX, int startY)
    {
        return new Spider(startX, startY);
    }
}