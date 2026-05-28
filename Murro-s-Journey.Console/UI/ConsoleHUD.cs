using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Events;

namespace Murro_s_Journey.Console.UI;

public class ConsoleHUD
{
    private Player _player;
    private int _currentHealth;
    private int _maxHealth;

    public ConsoleHUD(Player player)
    {
        _player = player;
        _currentHealth = player.Health;
        _maxHealth = player.MaxHealth;
        
        _player.HealthChanged += OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(object? sender, HealthChangedEventArgs e)
    {
        _currentHealth = e.CurrentHealth;
        _maxHealth = e.MaxHealth;
    }

    public string GetHealthBar()
    {
        int barLength = 20;
        int filledBars = (int)((float)_currentHealth / _maxHealth * barLength);
        
        string healthBar = new string('#', filledBars) + new string('.', barLength - filledBars);
        
        return $"[{healthBar}] {_currentHealth}/{_maxHealth} HP ({_currentHealth * 100 / _maxHealth}%)";
    }

    public void Dispose()
    {
        _player.HealthChanged -= OnPlayerHealthChanged;
    }
}