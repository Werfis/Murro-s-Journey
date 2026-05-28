namespace Murro_s_Journey.Console.Entities;

public abstract class Enemy : Entity
{
    protected int damage;
    protected int rewardExp;

    public int Damage => damage;
    public int RewardExp => rewardExp;

    protected Enemy(string name, int health, int damage, int rewardExp, int startX, int startY) 
        : base(name, health, startX, startY)
    {
        this.damage = damage;
        this.rewardExp = rewardExp;
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