using Murro_s_Journey.Console.Entities;
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

    private void ShowWelcome()
    {
        System.Console.Clear();
        System.Console.WriteLine("========================================");
        System.Console.WriteLine("||                                    ||");
        System.Console.WriteLine("||         Murro's Journey            ||");
        System.Console.WriteLine("||                                    ||");
        System.Console.WriteLine("||        Welcome to the forest       ||");
        System.Console.WriteLine("||                                    ||");
        System.Console.WriteLine("========================================");
        System.Console.WriteLine();
        System.Console.WriteLine($"  Map size: {MapWidth}x{MapHeight}");
        System.Console.WriteLine($"  Difficulty: {Difficulty}");
        System.Console.WriteLine($"  Starting health: {InitialPlayerHealth}");
        System.Console.WriteLine();
        System.Console.WriteLine("  Press any key to start...");
        System.Console.ReadKey();
    }

    private void ShowGoodbye()
    {
        System.Console.Clear();
        System.Console.WriteLine("========================================");
        System.Console.WriteLine("||                                    ||");
        System.Console.WriteLine("||        Thanks for playing!         ||");
        System.Console.WriteLine("||                                    ||");
        System.Console.WriteLine("||       See you in the forest!       ||");
        System.Console.WriteLine("||                                    ||");
        System.Console.WriteLine("========================================");
        System.Console.WriteLine();
        System.Console.WriteLine("  Press any key to exit...");
        System.Console.ReadKey();
    }

    public void Run()
    {
        ShowWelcome();
        System.Console.Clear();

        Game game = new Game(MapWidth, MapHeight, InitialPlayerHealth);
        game.Start();

        while (game.IsRunning)
        {
            game.Update();
            game.Draw();
            System.Threading.Thread.Sleep(100);
        }

        ShowGoodbye();
    }
}

public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard,
    Nightmare
}