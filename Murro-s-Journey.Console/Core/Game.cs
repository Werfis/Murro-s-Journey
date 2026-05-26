using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Core;

public class Game
{
    private bool isRunning;
    private Map currentMap;
    private Player player;

    public bool IsRunning => isRunning;

    public Game(int mapWidth, int mapHeight, int playerHealth)
    {
        isRunning = true;
        currentMap = new Map(mapWidth, mapHeight);

        player = new Player("Murro", mapWidth / 2, mapHeight / 2, playerHealth);
        currentMap.AddEntity(player);
        currentMap.Generate();
    }

    public void Start()
    {
        System.Console.WriteLine("Murro's Journey - Press ESC to exit");
    }

    public void Update()
    {
        if (System.Console.KeyAvailable)
        {
            var key = System.Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
            {
                Stop();
            }
            else if (key == ConsoleKey.W)
            {
                System.Console.WriteLine("Moving up");
            }
            else if (key == ConsoleKey.S)
            {
                System.Console.WriteLine("Moving down");
            }
            else if (key == ConsoleKey.A)
            {
                System.Console.WriteLine("Moving left");
            }
            else if (key == ConsoleKey.D)
            {
                System.Console.WriteLine("Moving right");
            }
        }
        currentMap.Update();
    }

    public void Draw()
    {
        System.Console.Clear();
        System.Console.WriteLine($"Health: {player.Health}/{player.MaxHealth} | Level: {player.Level}");
        System.Console.WriteLine();
        currentMap.Draw();
        System.Console.WriteLine();
        System.Console.WriteLine("WASD to move | ESC to exit");
    }

    public void Stop()
    {
        isRunning = false;
        System.Console.WriteLine("Thanks for playing!");
    }
}