using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Strategies;
using Murro_s_Journey.Console.UI;
using Murro_s_Journey.Console.Commands;

namespace Murro_s_Journey.Console.Core;

public class Game
{
    private bool isRunning;
    private Map currentMap;
    private Player player;
    private ConsoleHUD hud;

    public bool IsRunning => isRunning;
    public bool IsGameOver => !player.IsAlive();

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
        System.Console.WriteLine("Press Z to undo last action");
    }

    public void Update()
    {
        if (System.Console.KeyAvailable)
        {
            var key = System.Console.ReadKey(true).Key;
            
            if (key == ConsoleKey.Z)
            {
                GameManager.Instance.UndoLastCommand();
                return;
            }
            
            if (key == ConsoleKey.Escape)
            {
                Stop();
            }
            else if (key == ConsoleKey.W)
            {
                GameManager.Instance.ExecuteCommand(new MoveCommand(player, 0, -1));
            }
            else if (key == ConsoleKey.S)
            {
                GameManager.Instance.ExecuteCommand(new MoveCommand(player, 0, 1));
            }
            else if (key == ConsoleKey.A)
            {
                GameManager.Instance.ExecuteCommand(new MoveCommand(player, -1, 0));
            }
            else if (key == ConsoleKey.D)
            {
                GameManager.Instance.ExecuteCommand(new MoveCommand(player, 1, 0));
            }
            else if (key == ConsoleKey.H)
            {
                GameManager.Instance.ExecuteCommand(new HealCommand(player, 20));
            }
            else if (key == ConsoleKey.X)
            {
                GameManager.Instance.ExecuteCommand(new DamageCommand(player, 15));
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
        
        if (IsGameOver)
        {
            System.Console.WriteLine("Game Over! Press any key to exit...");
            System.Console.ReadKey();
            Stop();
        }
    }

    public void Draw()
    {
        System.Console.Clear();
        
        System.Console.WriteLine(hud.GetHealthBar());
        System.Console.WriteLine($"Level: {player.Level} | Exp: {player.Experience}");
        System.Console.WriteLine($"Undo history: {GameManager.Instance.GetHistoryCount()} commands");
        System.Console.WriteLine();
        
        currentMap.Draw();
        
        System.Console.WriteLine();
        System.Console.WriteLine("WASD to move | H = Heal | X = Take Damage | Z = Undo | ESC to exit");
    }

    public void Stop()
    {
        isRunning = false;
        hud.Dispose();
        System.Console.WriteLine("Thanks for playing!");
    }
}