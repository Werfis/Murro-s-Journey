namespace Murro_s_Journey.Console.Adapters;

public class DungeonGenerator
{
    private int[,] _dungeonData = new int[0, 0];
    private string _style;

    public DungeonGenerator(string style = "classic")
    {
        _style = style;
    }

    public int[,] CreateDungeon(int width, int height)
    {
        _dungeonData = new int[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    _dungeonData[x, y] = 1;
                }
                else
                {
                    _dungeonData[x, y] = 0;
                }
            }
        }
        
        Random random = new Random();
        for (int i = 0; i < 3; i++)
        {
            int tx = random.Next(2, width - 2);
            int ty = random.Next(2, height - 2);
            _dungeonData[tx, ty] = 2;
        }
        
        return _dungeonData;
    }

    public string GetDungeonStyle()
    {
        return $"Dungeon style: {_style} with treasure rooms";
    }
}