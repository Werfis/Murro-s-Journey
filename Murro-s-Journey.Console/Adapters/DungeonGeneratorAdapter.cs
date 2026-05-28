using Murro_s_Journey.Console.Interfaces;

namespace Murro_s_Journey.Console.Adapters;

public class DungeonGeneratorAdapter : IMapGenerator
{
    private DungeonGenerator _dungeonGenerator;
    private char[,] _map = new char[0, 0];

    public DungeonGeneratorAdapter(DungeonGenerator dungeonGenerator)
    {
        _dungeonGenerator = dungeonGenerator;
    }

    public void Generate(int width, int height)
    {
        int[,] dungeonData = _dungeonGenerator.CreateDungeon(width, height);
        _map = new char[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _map[x, y] = dungeonData[x, y] switch
                {
                    0 => '.',
                    1 => '#',
                    2 => '$',
                    _ => '.'
                };
            }
        }
    }

    public char[,] GetMap()
    {
        return _map;
    }

    public string GetDescription()
    {
        return $"{_dungeonGenerator.GetDungeonStyle()} (adapted to IMapGenerator)";
    }
}