using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Factories;

public class WolfFactory : EnemyFactory
{
    public override Enemy CreateEnemy(int startX, int startY)
    {
        return new Wolf(startX, startY);
    }
}