namespace Murro_s_Journey.Console.States;

public class GameContext
{
    private IGameState _currentState;

    public GameContext()
    {
        _currentState = new MenuState();
        _currentState.Enter();
    }

    public void ChangeState(IGameState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState.Update(this);
    }

    public IGameState GetCurrentState()
    {
        return _currentState;
    }
}