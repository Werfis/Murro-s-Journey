using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Strategies;

public class MeleeAttackStrategy : IEnemyBehavior
{
    private int _attackRange;

    public MeleeAttackStrategy(int attackRange = 1)
    {
        _attackRange = attackRange;
    }

    public void Execute(Enemy enemy, Player player)
    {
        int dx = Math.Abs(enemy.PosX - player.PosX);
        int dy = Math.Abs(enemy.PosY - player.PosY);
        int distance = dx + dy;

        if (distance <= _attackRange)
        {
            enemy.Attack(player);
            System.Console.WriteLine($"{enemy.Name} attacks {player.Name} for {enemy.Damage} damage!");
        }
        else
        {
            if (enemy.PosX < player.PosX) enemy.Move(1, 0);
            else if (enemy.PosX > player.PosX) enemy.Move(-1, 0);
            else if (enemy.PosY < player.PosY) enemy.Move(0, 1);
            else if (enemy.PosY > player.PosY) enemy.Move(0, -1);
            
            System.Console.WriteLine($"{enemy.Name} moves towards {player.Name}");
        }
    }

    public string GetDescription()
    {
        return $"Melee attack strategy (range: {_attackRange})";
    }
}