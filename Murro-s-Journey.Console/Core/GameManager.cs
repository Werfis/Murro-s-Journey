using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Factories;
using Murro_s_Journey.Console.Builders;
using Murro_s_Journey.Console.Decorators;
using Murro_s_Journey.Console.Interfaces;
using Murro_s_Journey.Console.Adapters;
using Murro_s_Journey.Console.Strategies;
using Murro_s_Journey.Console.Commands;

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
        _commandHistory = new Stack<ICommand>();
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

    private Stack<ICommand> _commandHistory;

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            ICommand lastCommand = _commandHistory.Pop();
            lastCommand.Undo();
            System.Console.WriteLine($"Undo: {lastCommand.GetDescription()}");
        }
        else
        {
            System.Console.WriteLine("Nothing to undo!");
        }
    }

    public void ClearHistory()
    {
        _commandHistory.Clear();
        System.Console.WriteLine("Command history cleared.");
    }

    public int GetHistoryCount()
    {
        return _commandHistory.Count;
    }

    private void DemonstrateDecoratorPattern()
    {
        System.Console.WriteLine("=== Damage Modifier System (Decorator Pattern) ===");
        System.Console.WriteLine();
        
        int baseDamage = 15;
        int enemyDamage = 25;
        
        System.Console.WriteLine($"Murro's base damage: {baseDamage}");
        System.Console.WriteLine($"Enemy deals to Murro: {enemyDamage} damage");
        System.Console.WriteLine();
        
        System.Console.WriteLine("--- Rage (attack increase) ---");
        
        IAttackModifier baseAttack = new BaseAttackModifier();
        System.Console.WriteLine($"No effects: {baseAttack.GetDescription()}");
        System.Console.WriteLine($"  Murro's damage: {baseAttack.GetModifiedDamage(baseDamage)}");
        
        IAttackModifier rageAttack = new RageDecorator(baseAttack, 1.5f);
        System.Console.WriteLine($"With Rage: {rageAttack.GetDescription()}");
        System.Console.WriteLine($"  Murro's damage: {rageAttack.GetModifiedDamage(baseDamage)}");
        System.Console.WriteLine($"  Calculation: 15 x 1.5 = 22");
        System.Console.WriteLine();
        
        System.Console.WriteLine("--- Nature Guardian (damage protection) ---");
        
        IDefenseModifier baseDefense = new BaseDefenseModifier();
        System.Console.WriteLine($"No defense: {baseDefense.GetDescription()}");
        System.Console.WriteLine($"  Murro takes: {baseDefense.GetModifiedDamage(enemyDamage)} damage");
        
        IDefenseModifier natureDefense = new NatureGuardianDecorator(baseDefense, 30);
        System.Console.WriteLine($"With Nature Guardian: {natureDefense.GetDescription()}");
        
        System.Console.WriteLine();
        System.Console.WriteLine("Defense demonstration (5 enemy attacks):");
        for (int i = 0; i < 5; i++)
        {
            int finalDamage = natureDefense.GetModifiedDamage(enemyDamage);
            System.Console.WriteLine($"  Attack {i + 1}: Murro takes {finalDamage} damage");
        }
        
        System.Console.WriteLine();
    }

    private void DemonstrateBuilderPattern()
    {
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
    }

    private void DemonstrateAdapterPattern()
    {
        System.Console.WriteLine("=== Adapter Pattern Demo ===");
        System.Console.WriteLine();
        
        int width = 10;
        int height = 8;
        
        System.Console.WriteLine("--- Original Map Generator ---");
        IMapGenerator originalGenerator = new SimpleMapGenerator();
        originalGenerator.Generate(width, height);
        char[,] originalMap = originalGenerator.GetMap();
        
        System.Console.WriteLine($"Description: {originalGenerator.GetDescription()}");
        System.Console.WriteLine("Generated map:");
        PrintMap(originalMap);
        System.Console.WriteLine();
        
        System.Console.WriteLine("--- Adapted Dungeon Generator ---");
        DungeonGenerator thirdPartyGenerator = new DungeonGenerator("dark forest");
        IMapGenerator adaptedGenerator = new DungeonGeneratorAdapter(thirdPartyGenerator);
        adaptedGenerator.Generate(width, height);
        char[,] adaptedMap = adaptedGenerator.GetMap();
        
        System.Console.WriteLine($"Description: {adaptedGenerator.GetDescription()}");
        System.Console.WriteLine("Generated map:");
        PrintMap(adaptedMap);
        System.Console.WriteLine();
        
        System.Console.WriteLine("Both generators work with the same interface!");
        System.Console.WriteLine();
    }

    private void PrintMap(char[,] map)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                System.Console.Write(map[x, y]);
            }
            System.Console.WriteLine();
        }
    }

    private void DemonstrateStrategyPattern()
    {
        System.Console.WriteLine("=== Strategy Pattern Demo (Enemy Behaviors) ===");
        System.Console.WriteLine();
        
        var testEnemy = new Wolf(5, 5);
        var player = new Player("Murro", 10, 5, 100);
        
        System.Console.WriteLine($"Enemy: {testEnemy.Name}");
        System.Console.WriteLine($"Player: {player.Name} at position ({player.PosX}, {player.PosY})");
        System.Console.WriteLine();
        
        System.Console.WriteLine("--- Melee Attack Strategy ---");
        testEnemy.SetBehavior(new MeleeAttackStrategy(1));
        testEnemy.ExecuteBehavior(player);
        System.Console.WriteLine($"Player health: {player.Health}");
        System.Console.WriteLine();
        
        System.Console.WriteLine("--- Changing to Passive Strategy ---");
        testEnemy.SetBehavior(new PassiveBehaviorStrategy());
        testEnemy.ExecuteBehavior(player);
        System.Console.WriteLine($"Player health: {player.Health} (unchanged)");
        System.Console.WriteLine();
        
        System.Console.WriteLine("--- Changing back to Melee Strategy ---");
        testEnemy.SetBehavior(new MeleeAttackStrategy(1));
        testEnemy.ExecuteBehavior(player);
        System.Console.WriteLine($"Player health: {player.Health}");
        
        System.Console.WriteLine();
    }

    private void DemonstrateCommandPattern()
    {
        System.Console.WriteLine("=== Command Pattern Demo (Undo System) ===");
        System.Console.WriteLine();
        
        var testPlayer = new Player("Test", 10, 5, 100);
        var commands = new List<ICommand>
        {
            new MoveCommand(testPlayer, 1, 0),
            new MoveCommand(testPlayer, 0, -1),
            new HealCommand(testPlayer, 20),
            new MoveCommand(testPlayer, -1, 0),
            new DamageCommand(testPlayer, 15),
            new HealCommand(testPlayer, 10)
        };
        
        System.Console.WriteLine("--- Executing Commands ---");
        foreach (var cmd in commands)
        {
            cmd.Execute();
        }
        
        System.Console.WriteLine();
        System.Console.WriteLine($"Final player position: ({testPlayer.PosX}, {testPlayer.PosY})");
        System.Console.WriteLine($"Final player health: {testPlayer.Health}");
        
        System.Console.WriteLine();
        System.Console.WriteLine("--- Undo Operations ---");
        
        for (int i = 0; i < commands.Count + 1; i++)
        {
            if (_commandHistory.Count > 0)
            {
                var lastCmd = _commandHistory.Pop();
                lastCmd.Undo();
            }
            else
            {
                System.Console.WriteLine("History is empty!");
            }
        }
        
        System.Console.WriteLine();
        System.Console.WriteLine($"Final player position after undo: ({testPlayer.PosX}, {testPlayer.PosY})");
        System.Console.WriteLine($"Final player health after undo: {testPlayer.Health}");
        System.Console.WriteLine();
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

        DemonstrateDecoratorPattern();
        DemonstrateBuilderPattern();
        DemonstrateAdapterPattern();
        DemonstrateStrategyPattern();
        DemonstrateCommandPattern();
        
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