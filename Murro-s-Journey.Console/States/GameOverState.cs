namespace Murro_s_Journey.Console.States;

public class GameOverState : IGameState
{
    private int _selectedOption = 0;
    private string[] _options = { "Restart Game", "Main Menu", "Exit" };

    public void Enter()
    {
        System.Console.Clear();
        System.Console.WriteLine("==================================");
        System.Console.WriteLine("           GAME OVER");
        System.Console.WriteLine("==================================");
        System.Console.WriteLine();
        DrawMenu();
    }

    private void DrawMenu()
    {
        for (int i = 0; i < _options.Length; i++)
        {
            if (i == _selectedOption)
            {
                System.Console.WriteLine($"> {_options[i]}");
            }
            else
            {
                System.Console.WriteLine($"  {_options[i]}");
            }
        }
        System.Console.WriteLine();
        System.Console.WriteLine("Use Arrow Keys to navigate, Enter to select");
    }

    public void Update(GameContext context)
    {
        if (System.Console.KeyAvailable)
        {
            var key = System.Console.ReadKey(true).Key;
            
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _selectedOption = (_selectedOption - 1 + _options.Length) % _options.Length;
                    System.Console.Clear();
                    System.Console.WriteLine("==================================");
                    System.Console.WriteLine("           GAME OVER");
                    System.Console.WriteLine("==================================");
                    System.Console.WriteLine();
                    DrawMenu();
                    break;
                    
                case ConsoleKey.DownArrow:
                    _selectedOption = (_selectedOption + 1) % _options.Length;
                    System.Console.Clear();
                    System.Console.WriteLine("==================================");
                    System.Console.WriteLine("           GAME OVER");
                    System.Console.WriteLine("==================================");
                    System.Console.WriteLine();
                    DrawMenu();
                    break;
                    
                case ConsoleKey.Enter:
                    switch (_selectedOption)
                    {
                        case 0:
                            context.ChangeState(new GameState());
                            break;
                        case 1:
                            context.ChangeState(new MenuState());
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                    }
                    break;
            }
        }
    }

    public void Exit()
    {
    }

    public string GetName()
    {
        return "GameOver";
    }
}