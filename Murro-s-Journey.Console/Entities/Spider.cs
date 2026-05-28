namespace Murro_s_Journey.Console.Entities;

public class Spider : Enemy
{
    private Random random = new Random();

    public Spider(int startX, int startY) 
        : base("Spider", 30, 12, 45, startX, startY)
    {
    }

    public override void Attack(Player target)
    {
        target.TakeDamage(damage);
        
        if (random.Next(100) < 30)
        {
            System.Console.WriteLine($"Sad memories of the past hit {target.Name} straight in the heart...");
            target.TakeDamage(5);
        }
    }

    public override string GetDescription()
    {
        return $"Spider - Health: {health}, Damage: {damage} | Violetta? Oh... No, you're not...";
    }

    public override void Draw()
    {
        System.Console.Write("S");
    }
}