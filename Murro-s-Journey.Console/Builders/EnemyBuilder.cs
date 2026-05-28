using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Builders;

public class EnemyBuilder
{
    private string _name;
    private int _health;
    private int _damage;
    private int _rewardExp;
    private int _posX;
    private int _posY;
    private string _enemyType;

    public EnemyBuilder()
    {
        _name = "Generic Enemy";
        _health = 50;
        _damage = 10;
        _rewardExp = 30;
        _posX = 0;
        _posY = 0;
        _enemyType = "Default";
    }

    public EnemyBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public EnemyBuilder SetHealth(int health)
    {
        _health = health;
        return this;
    }

    public EnemyBuilder SetDamage(int damage)
    {
        _damage = damage;
        return this;
    }

    public EnemyBuilder SetRewardExp(int rewardExp)
    {
        _rewardExp = rewardExp;
        return this;
    }

    public EnemyBuilder SetPosition(int x, int y)
    {
        _posX = x;
        _posY = y;
        return this;
    }

    public EnemyBuilder SetType(string enemyType)
    {
        _enemyType = enemyType;
        return this;
    }

    public Enemy Build()
    {
        switch (_enemyType.ToLower())
        {
            case "wolf":
                return new Wolf(_posX, _posY);
            case "spider":
                return new Spider(_posX, _posY);
            default:
                return new CustomEnemy(_name, _health, _damage, _rewardExp, _posX, _posY);
        }
    }
}

public class CustomEnemy : Enemy
{
    public CustomEnemy(string name, int health, int damage, int rewardExp, int startX, int startY) 
        : base(name, health, damage, rewardExp, startX, startY)
    {
    }

    public override void Attack(Player target)
    {
        target.TakeDamage(damage);
        System.Console.WriteLine($"{name} attacks for {damage} damage!");
    }

    public override string GetDescription()
    {
        return $"{name} - Health: {health}, Damage: {damage} | A custom enemy";
    }

    public override void Draw()
    {
        System.Console.Write("C");
    }
}