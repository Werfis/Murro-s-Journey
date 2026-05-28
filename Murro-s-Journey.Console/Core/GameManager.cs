using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Factories;
using Murro_s_Journey.Console.Builders;

namespace Murro_s_Journey.Console.Core;

public sealed class GameManager
{
    private static GameManager? _instance;
    private static readonly object _lock = new object();

    private GameManager()
    {
        MapWidth = 20;
        MapHeight = 10;
        Difficulty = DifficultyLevel.Normal;
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameManager();
                    }
                }
            }
            return _instance;
        }
    }

    public int MapWidth { get; set; }
    public int MapHeight { get; set; }

    public DifficultyLevel Difficulty { get; set; }

    public int InitialPlayerHealth
    {
        get
        {
            switch (Difficulty)
            {
                case DifficultyLevel.Easy: return 150;
                case DifficultyLevel.Normal: return 100;
                case DifficultyLevel.Hard: return 70;
                case DifficultyLevel.Nightmare: return 50;
                default: return 100;
            }
        }
    }

    public int EnemyDamageMultiplier
    {
        get
        {
            switch (Difficulty)
            {
                case DifficultyLevel.Easy: return 1;
                case DifficultyLevel.Normal: return 1;
                case DifficultyLevel.Hard: return 2;
                case DifficultyLevel.Nightmare: return 3;
                default: return 1;
            }
        }
    }

    public void Run()
    {
        System.Console.WriteLine("==================================");
        System.Console.WriteLine("Welcome to the forest, Murro");
        System.Console.WriteLine($"Map size: {MapWidth}x{MapHeight}");
        System.Console.WriteLine($"Difficulty: {Difficulty}");
        System.Console.WriteLine($"Starting health: {InitialPlayerHealth}");
        System.Console.WriteLine("==================================");
        System.Console.WriteLine();

        System.Console.WriteLine("=== Enemy Builder Demo ===");
        
        Enemy boss = new EnemyBuilder()
            .SetName("Smiley Face")
            .SetHealth(200)
            .SetDamage(30)
            .SetRewardExp(300)
            .SetPosition(10, 5)
            .SetType("custom")
            .Build();
        
        System.Console.WriteLine($"Created: {boss.Name}");
        System.Console.WriteLine($"  {boss.GetDescription()}");
        
        Enemy wolf = new EnemyBuilder()
            .SetType("wolf")
            .SetPosition(5, 5)
            .Build();
        
        System.Console.WriteLine();
        System.Console.WriteLine($"Created: {wolf.Name}");
        System.Console.WriteLine($"  {wolf.GetDescription()}");
        
        Enemy spider = new EnemyBuilder()
            .SetType("spider")
            .SetPosition(8, 7)
            .Build();
        
        System.Console.WriteLine();
        System.Console.WriteLine($"Created: {spider.Name}");
        System.Console.WriteLine($"  {spider.GetDescription()}");
        
        Enemy fastEnemy = new EnemyBuilder()
            .SetName("Sergi")
            .SetHealth(60)
            .SetDamage(25)
            .SetRewardExp(120)
            .SetPosition(12, 3)
            .SetType("custom")
            .Build();
        
        System.Console.WriteLine();
        System.Console.WriteLine($"Created: {fastEnemy.Name}");
        System.Console.WriteLine($"  {fastEnemy.GetDescription()}");
        
        System.Console.WriteLine();
        System.Console.WriteLine("=== Attack Demo ===");
        
        Player demoPlayer = new Player("Murro", 15, 5, 100);
        
        System.Console.WriteLine($"Player health before boss attack: {demoPlayer.Health}");
        boss.Attack(demoPlayer);
        System.Console.WriteLine($"Player health after boss attack: {demoPlayer.Health}");
        
        System.Console.WriteLine();
        
        System.Console.WriteLine($"Player health before wolf attack: {demoPlayer.Health}");
        wolf.Attack(demoPlayer);
        System.Console.WriteLine($"Player health after wolf attack: {demoPlayer.Health}");
        
        System.Console.WriteLine();
        
        System.Console.WriteLine($"Player health before spider attack: {demoPlayer.Health}");
        spider.Attack(demoPlayer);
        System.Console.WriteLine($"Player health after spider attack: {demoPlayer.Health}");
        
        System.Console.WriteLine();
        System.Console.WriteLine("Press any key to start the journey...");
        System.Console.ReadKey();

        Game game = new Game(MapWidth, MapHeight, InitialPlayerHealth);
        game.Start();

        while (game.IsRunning)
        {
            game.Update();
            game.Draw();
            System.Threading.Thread.Sleep(100);
        }
    }
}

public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard,
    Nightmare
}