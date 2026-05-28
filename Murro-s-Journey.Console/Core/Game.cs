using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Strategies;

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
        
        var wolf = new Wolf(5, 5);
        wolf.SetBehavior(new MeleeAttackStrategy());
        currentMap.AddEntity(wolf);
        
        var spider = new Spider(12, 5);
        spider.SetBehavior(new PassiveBehaviorStrategy());
        currentMap.AddEntity(spider);
    }

    public void Start()
    {
        System.Console.WriteLine("Murro's Journey - Press ESC to exit");
        System.Console.WriteLine("Controls: WASD to move");
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
                player.Move(0, -1);
                System.Console.WriteLine("Moving up");
            }
            else if (key == ConsoleKey.S)
            {
                player.Move(0, 1);
                System.Console.WriteLine("Moving down");
            }
            else if (key == ConsoleKey.A)
            {
                player.Move(-1, 0);
                System.Console.WriteLine("Moving left");
            }
            else if (key == ConsoleKey.D)
            {
                player.Move(1, 0);
                System.Console.WriteLine("Moving right");
            }
        }
        
        foreach (var entity in currentMap.GetAllEntities())
        {
            if (entity is Enemy enemy && enemy.Behavior != null)
            {
                enemy.ExecuteBehavior(player);
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