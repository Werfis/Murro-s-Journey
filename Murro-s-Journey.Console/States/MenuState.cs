namespace Murro_s_Journey.Console.States;

public class MenuState : IGameState
{
    public void Enter()
    {
        System.Console.Clear();
        System.Console.WriteLine("==================================");
        System.Console.WriteLine("        Murro's Journey");
        System.Console.WriteLine("==================================");
        System.Console.WriteLine();
        System.Console.WriteLine("1. Start Game");
        System.Console.WriteLine("2. Exit");
        System.Console.WriteLine();
        System.Console.WriteLine("Press 1 to start, 2 to exit...");
    }

    public void Update(GameContext context)
    {
        if (System.Console.KeyAvailable)
        {
            var key = System.Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
            {
                context.ChangeState(new GameState());
            }
            else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
            {
                Environment.Exit(0);
            }
        }
    }

    public void Exit()
    {
        System.Console.WriteLine("Starting game...");
    }

    public string GetName()
    {
        return "Menu";
    }
}