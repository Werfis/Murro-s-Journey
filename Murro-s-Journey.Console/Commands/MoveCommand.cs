using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Commands;

public class MoveCommand : ICommand
{
    private Player _player;
    private int _deltaX;
    private int _deltaY;
    private int _oldX;
    private int _oldY;

    public MoveCommand(Player player, int deltaX, int deltaY)
    {
        _player = player;
        _deltaX = deltaX;
        _deltaY = deltaY;
    }

    public void Execute()
    {
        _oldX = _player.PosX;
        _oldY = _player.PosY;
        
        _player.Move(_deltaX, _deltaY);
        
        System.Console.WriteLine($"Moved from ({_oldX},{_oldY}) to ({_player.PosX},{_player.PosY})");
    }

    public void Undo()
    {
        _player.SetPosition(_oldX, _oldY);
        System.Console.WriteLine($"Undo: Returned to ({_oldX},{_oldY})");
    }

    public string GetDescription()
    {
        return $"Move ({_deltaX},{_deltaY})";
    }
}