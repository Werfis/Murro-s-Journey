using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Items;

namespace Murro_s_Journey.Console.Core;

public class Map
{
    private int width;
    private int height;
    private List<Entity> entities;
    private List<Item> items;

    public int Width => width;
    public int Height => height;

    public Map(int width, int height)
    {
        this.width = width;
        this.height = height;
        entities = new List<Entity>();
        items = new List<Item>();
    }

    public void Generate()
    {
    }

    public void AddEntity(Entity entity) => entities.Add(entity);
    public void AddItem(Item item) => items.Add(item);

    public Entity? GetEntityAt(int x, int y)
    {
        return entities.FirstOrDefault(e => e.PosX == x && e.PosY == y);
    }

    public Item? GetItemAt(int x, int y)
    {
        return items.FirstOrDefault(i => i.PosX == x && i.PosY == y);
    }

    public List<Entity> GetAllEntities()
    {
        return entities.ToList();
    }

    public void Update()
    {
        foreach (var entity in entities.ToList())
        {
            entity.Update();
        }
    }

    public void Draw()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var entity = GetEntityAt(x, y);
                var item = GetItemAt(x, y);
                
                if (entity != null)
                    entity.Draw();
                else if (item != null)
                    item.Draw();
                else
                    System.Console.Write(".");
            }
            System.Console.WriteLine();
        }
    }
}