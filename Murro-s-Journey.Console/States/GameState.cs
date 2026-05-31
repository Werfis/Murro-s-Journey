using Murro_s_Journey.Console.Core;

namespace Murro_s_Journey.Console.States;

public class GameState : IGameState
{
    private Game? _game;
    private bool _isPaused;
    private bool _isInitialized;

    public void Enter()
    {
        _isPaused = false;
        _isInitialized = false;
        _game = new Game(20, 10, 100);
        _game.Start();
        _isInitialized = true;
    }

    public void Update(GameContext context)
    {
        if (!_isInitialized || _game == null) return;
        
        if (_isPaused)
        {
            if (System.Console.KeyAvailable)
            {
                var key = System.Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    _isPaused = false;
                    System.Console.WriteLine("Game resumed");
                }
                else if (key == ConsoleKey.M)
                {
                    context.ChangeState(new MenuState());
                }
            }
            
            System.Console.SetCursorPosition(0, 0);
            System.Console.WriteLine("=== PAUSED ===");
            System.Console.WriteLine("Press ESC to resume");
            System.Console.WriteLine("Press M for Menu");
            return;
        }
        
        if (System.Console.KeyAvailable)
        {
            var key = System.Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
            {
                _isPaused = true;
                System.Console.WriteLine("Game paused - Press ESC to resume");
                return;
            }
        }
        
        _game.Update();
        _game.Draw();
        
        if (_game.IsGameOver)
        {
            context.ChangeState(new GameOverState());
        }
    }

    public void Exit()
    {
        _game = null;
        _isInitialized = false;
    }

    public string GetName()
    {
        return "Game";
    }
}