using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Core;

public class Game
{
    private bool isRunning;
    private Map currentMap;
    private Player player;

    public bool IsRunning => isRunning;

    public Game()
    {
        isRunning = true;
        currentMap = new Map(20, 10);
        player = new Player("Murro", 10, 5);
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
        System.Console.WriteLine("ESC to exit");
    }

    public void Stop()
    {
        isRunning = false;
        System.Console.WriteLine("Thanks for playing!");
    }
}