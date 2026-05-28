using Murro_s_Journey.Console.Interfaces;

namespace Murro_s_Journey.Console.Core;

public class SimpleMapGenerator : IMapGenerator
{
    private char[,] _map = new char[0, 0];

    public void Generate(int width, int height)
    {
        _map = new char[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Random random = new Random();
                _map[x, y] = random.Next(100) < 10 ? '#' : '.';
            }
        }
    }

    public char[,] GetMap()
    {
        return _map;
    }

    public string GetDescription()
    {
        return "Simple random map generator";
    }
}