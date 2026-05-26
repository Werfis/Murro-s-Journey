using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Items;

public abstract class Item
{
    protected string name;
    protected string description;
    protected int posX;
    protected int posY;

    public string Name => name;
    public string Description => description;
    public int PosX => posX;
    public int PosY => posY;

    protected Item(string name, string description, int posX, int posY)
    {
        this.name = name;
        this.description = description;
        this.posX = posX;
        this.posY = posY;
    }

    public abstract void Use(Player user);
    public abstract void OnPickup();
    public abstract void Draw();
}