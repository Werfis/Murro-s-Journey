namespace Murro_s_Journey.Console.States;

public interface IGameState
{
    void Enter();
    void Update(GameContext context);
    void Exit();
    string GetName();
}