using Murro_s_Journey.Console.Entities;
using Murro_s_Journey.Console.Events;

namespace Murro_s_Journey.Console.UI;

public class ConsoleHUD
{
    private Player _player;

    public ConsoleHUD(Player player)
    {
        _player = player;
        
        _player.HealthChanged += OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(object? sender, HealthChangedEventArgs e)
    {
        DrawHealthBar(e.CurrentHealth, e.MaxHealth);
    }

    public void DrawHealthBar(int currentHealth, int maxHealth)
    {
        int barLength = 20;
        int filledBars = (int)((float)currentHealth / maxHealth * barLength);
        
        string healthBar = new string('█', filledBars) + new string('░', barLength - filledBars);
        
        System.Console.WriteLine($"[{healthBar}] {currentHealth}/{maxHealth} HP ({currentHealth * 100 / maxHealth}%)");
    }

    public void Dispose()
    {
        _player.HealthChanged -= OnPlayerHealthChanged;
    }
}