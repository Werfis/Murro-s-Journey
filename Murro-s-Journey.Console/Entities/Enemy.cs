namespace Murro_s_Journey.Console.Entities;

public class Enemy : Entity
{
    private int damage;
    private int rewardExp;

    public Enemy(string name, int health, int damage, int rewardExp, int startX, int startY) 
        : base(name, health, startX, startY)
    {
        this.damage = damage;
        this.rewardExp = rewardExp;
    }

    public void Attack(Player target)
    {
        target.TakeDamage(damage);
    }

    public int GetRewardExp() => rewardExp;

    public override void Update()
    {
        // TODO: Enemy AI
    }

    public override void Draw()
    {
        System.Console.Write("E");
    }
}