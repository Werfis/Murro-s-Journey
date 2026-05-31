namespace Murro_s_Journey.Console.Entities;

public class Wolf : Enemy
{
    private Random random = new Random();

    public Wolf(int startX, int startY) 
        : base("Wolf", 40, 8, 30, startX, startY)
    {
    }

    public override void Attack(Player target)
    {
        int finalDamage = damage;
        
        if (random.Next(100) < 20)
        {
            finalDamage = damage * 2;
            System.Console.WriteLine($"The wolf makes a big bite! {finalDamage} damage!");
        }
    }

    public override string GetDescription()
    {
        return $"Wolf - Health: {health}, Damage: {damage} | A wolf is like a dog, but wild. It makes big bites.";
    }

    public override void Draw()
    {
        System.Console.Write("W");
    }
}