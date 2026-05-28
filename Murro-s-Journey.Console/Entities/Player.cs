namespace Murro_s_Journey.Console.Entities;

public class Player : Entity
{
    private int experience;
    private int level;

    public int Experience => experience;
    public int Level => level;

    public Player(string name, int startX, int startY, int startHealth) 
        : base(name, startHealth, startX, startY)
    {
        experience = 0;
        level = 1;
    }

    public void GainExp(int amount)
    {
        experience += amount;
        if (experience >= level * 100)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        experience = 0;
        maxHealth += 20;
        health = maxHealth;
        System.Console.WriteLine($"Level up! Now level {level}");
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

    public override void Update()
    {
    }

    public override void Draw()
    {
        System.Console.Write("@");
    }
}