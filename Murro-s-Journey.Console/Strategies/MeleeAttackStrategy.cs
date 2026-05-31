using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Strategies;

public class MeleeAttackStrategy : IEnemyBehavior
{
    private int _attackRange;
    private int _attackDelay;
    private const int DELAY_FRAMES = 20;

    public MeleeAttackStrategy(int attackRange = 1)
    {
        _attackRange = attackRange;
        _attackDelay = 0;
    }

    public void Execute(Enemy enemy, Player player)
    {
        int dx = Math.Abs(enemy.PosX - player.PosX);
        int dy = Math.Abs(enemy.PosY - player.PosY);
        int distance = dx + dy;

        if (distance <= _attackRange)
        {
            if (_attackDelay > 0)
            {
                _attackDelay--;
                System.Console.WriteLine($"{enemy.Name} is preparing next attack...");
                return;
            }
            
            enemy.Attack(player);
            System.Console.WriteLine($"{enemy.Name} attacks {player.Name} for {enemy.Damage} damage!");
            
            _attackDelay = DELAY_FRAMES;
        }
        else
        {
            if (_attackDelay == 0)
            {
                if (enemy.PosX < player.PosX) enemy.Move(1, 0);
                else if (enemy.PosX > player.PosX) enemy.Move(-1, 0);
                else if (enemy.PosY < player.PosY) enemy.Move(0, 1);
                else if (enemy.PosY > player.PosY) enemy.Move(0, -1);
                
                System.Console.WriteLine($"{enemy.Name} moves towards {player.Name}");
            }
            else
            {
                _attackDelay--;
                System.Console.WriteLine($"{enemy.Name} is recovering after attack...");
            }
        }
    }

    public string GetDescription()
    {
        return $"Melee attack strategy (range: {_attackRange}, delay: {DELAY_FRAMES} frames)";
    }
}