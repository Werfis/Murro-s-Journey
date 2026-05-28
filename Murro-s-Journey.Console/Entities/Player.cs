using Murro_s_Journey.Console.Events;

namespace Murro_s_Journey.Console.Entities;

public class Player : Entity
{
    private int experience;
    private int level;

    public int Experience => experience;
    public int Level => level;

    public event EventHandler<HealthChangedEventArgs>? HealthChanged;

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
        Heal(20);
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

    public override int TakeDamage(int damage)
    {
        int oldHealth = health;
        int actualDamage = base.TakeDamage(damage);
        
        if (oldHealth != health)
        {
            OnHealthChanged(new HealthChangedEventArgs(health, maxHealth));
        }
        
        return actualDamage;
    }

    public override int Heal(int amount)
    {
        int oldHealth = health;
        int actualHeal = base.Heal(amount);
        
        if (oldHealth != health)
        {
            OnHealthChanged(new HealthChangedEventArgs(health, maxHealth));
        }
        
        return actualHeal;
    }

    protected virtual void OnHealthChanged(HealthChangedEventArgs e)
    {
        HealthChanged?.Invoke(this, e);
    }

    public override void Update()
    {
    }

    public override void Draw()
    {
        System.Console.Write("@");
    }
}