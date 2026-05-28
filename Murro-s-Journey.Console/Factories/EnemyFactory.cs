using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Factories;

public abstract class EnemyFactory
{
    public abstract Enemy CreateEnemy(int startX, int startY);
}