using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Commands;

public class HealCommand : ICommand
{
    private Player _player;
    private int _healAmount;
    private int _oldHealth;

    public HealCommand(Player player, int healAmount)
    {
        _player = player;
        _healAmount = healAmount;
    }

    public void Execute()
    {
        _oldHealth = _player.Health;
        _player.Heal(_healAmount);
        System.Console.WriteLine($"Healed for {_healAmount} HP. Health: {_player.Health}");
    }

    public void Undo()
    {
        int damage = _player.Health - _oldHealth;
        _player.TakeDamage(damage);
        System.Console.WriteLine($"Undo: Health returned to {_player.Health}");
    }

    public string GetDescription()
    {
        return $"Heal +{_healAmount} HP";
    }
}