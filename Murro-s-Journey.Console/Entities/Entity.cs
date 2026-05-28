namespace Murro_s_Journey.Console.Entities;

public abstract class Entity
{
    protected string name;
    protected int health;
    protected int maxHealth;
    protected int posX;
    protected int posY;

    public string Name => name;
    public int Health => health;
    public int MaxHealth => maxHealth;
    public int PosX => posX;
    public int PosY => posY;

    protected Entity(string name, int maxHealth, int startX, int startY)
    {
        this.name = name;
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.posX = startX;
        this.posY = startY;
    }

    public virtual int TakeDamage(int damage)
    {
        if (damage < 0) return 0;
        
        int actualDamage = Math.Min(damage, health);
        health -= damage;
        if (health < 0) health = 0;
        
        return actualDamage;
    }

    public virtual int Heal(int amount)
    {
        if (amount < 0) return 0;
        if (!IsAlive()) return 0;
        
        int oldHealth = health;
        health += amount;
        if (health > maxHealth) health = maxHealth;
        
        return health - oldHealth;
    }

    public bool IsAlive() => health > 0;

    public abstract void Update();
    public abstract void Draw();
}