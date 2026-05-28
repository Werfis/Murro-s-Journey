namespace Murro_s_Journey.Console.Interfaces;

public interface IMapGenerator
{
    void Generate(int width, int height);
    char[,] GetMap();
    string GetDescription();
}