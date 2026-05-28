using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Factories;

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

        System.Console.WriteLine("=== Enemy Factory Demo ===");
        
        EnemyFactory wolfFactory = new WolfFactory();
        EnemyFactory spiderFactory = new SpiderFactory();
        
        Enemy wolf = wolfFactory.CreateEnemy(5, 5);
        Enemy spider = spiderFactory.CreateEnemy(8, 5);
        
        System.Console.WriteLine($"Created: {wolf.Name}");
        System.Console.WriteLine($"  {wolf.GetDescription()}");
        System.Console.WriteLine();
        System.Console.WriteLine($"Created: {spider.Name}");
        System.Console.WriteLine($"  {spider.GetDescription()}");
        
        System.Console.WriteLine();
        System.Console.WriteLine("=== Attack Demo ===");
        
        Player demoPlayer = new Player("Murro", 15, 5, 100);
        System.Console.WriteLine($"Player health before Wolf attack: {demoPlayer.Health}");
        wolf.Attack(demoPlayer);
        System.Console.WriteLine($"Player health after Wolf attack: {demoPlayer.Health}");
        
        System.Console.WriteLine();
        
        System.Console.WriteLine($"Player health before Spider attack: {demoPlayer.Health}");
        spider.Attack(demoPlayer);
        System.Console.WriteLine($"Player health after Spider attack: {demoPlayer.Health}");
        
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