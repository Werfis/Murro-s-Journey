namespace Murro_s_Journey.Console.Commands;

public interface ICommand
{
    void Execute();
    void Undo();
    string GetDescription();
}