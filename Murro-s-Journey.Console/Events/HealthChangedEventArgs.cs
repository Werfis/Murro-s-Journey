namespace Murro_s_Journey.Console.Events;

public class HealthChangedEventArgs : EventArgs
{
    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    public int HealthPercentage { get; }

    public HealthChangedEventArgs(int currentHealth, int maxHealth)
    {
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
        HealthPercentage = (int)((float)currentHealth / maxHealth * 100);
    }
}