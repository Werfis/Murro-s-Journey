using Murro_s_Journey.Console.Strategies;

namespace Murro_s_Journey.Console.Entities;

public abstract class Enemy : Entity
{
    protected int damage;
    protected int rewardExp;

    public int Damage => damage;
    public int RewardExp => rewardExp;

    private IEnemyBehavior _behavior;

    public IEnemyBehavior Behavior
    {
        get => _behavior;
        set => _behavior = value;
    }

    protected Enemy(string name, int health, int damage, int rewardExp, int startX, int startY) 
        : base(name, health, startX, startY)
    {
        this.damage = damage;
        this.rewardExp = rewardExp;
        _behavior = new MeleeAttackStrategy();
    }

    public void SetBehavior(IEnemyBehavior behavior)
    {
        _behavior = behavior;
        System.Console.WriteLine($"{name} changed behavior to: {behavior.GetDescription()}");
    }

    public void ExecuteBehavior(Player player)
    {
        _behavior?.Execute(this, player);
    }

    public void Move(int deltaX, int deltaY)
    {
        int newX = posX + deltaX;
        int newY = posY + deltaY;
        
        if (newX >= 0 && newX < 20 && newY >= 0 && newY < 10)
        {
            posX = newX;
            posY = newY;
        }
    }

    public abstract void Attack(Player target);
    public abstract string GetDescription();

    public override void Update()
    {
    }

    public override void Draw()
    {
        System.Console.Write("E");
    }
}