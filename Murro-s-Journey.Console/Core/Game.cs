using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Strategies;
using Murro_s_Journey.Console.UI;

namespace Murro_s_Journey.Console.Core;

public class Game
{
    private bool isRunning;
    private Map currentMap;
    private Player player;
    private ConsoleHUD hud;

    public bool IsRunning => isRunning;

    public Game(int mapWidth, int mapHeight, int playerHealth)
    {
        isRunning = true;
        currentMap = new Map(mapWidth, mapHeight);
        
        player = new Player("Murro", mapWidth / 2, mapHeight / 2, playerHealth);
        currentMap.AddEntity(player);
        currentMap.Generate();
        
        hud = new ConsoleHUD(player);
        
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
        System.Console.WriteLine("Press H to heal, Press X to take damage (demo events)");
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
            else if (key == ConsoleKey.H)
            {
                player.Heal(20);
                System.Console.WriteLine("You healed 20 HP!");
            }
            else if (key == ConsoleKey.X)
            {
                player.TakeDamage(15);
                System.Console.WriteLine("You took 15 damage!");
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
        // Display player stats
        System.Console.WriteLine($"Level: {player.Level} | Exp: {player.Experience}");
        System.Console.WriteLine();
        currentMap.Draw();
        System.Console.WriteLine();
        System.Console.WriteLine("WASD to move | H = Heal | X = Take Damage | ESC to exit");
    }

    public void Stop()
    {
        isRunning = false;
        hud.Dispose();
        System.Console.WriteLine("Thanks for playing!");
    }
}