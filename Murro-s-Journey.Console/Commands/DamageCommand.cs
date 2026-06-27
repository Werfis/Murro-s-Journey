using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Console.Commands;

public class DamageCommand : ICommand
{
    private Player _player;
    private int _damageAmount;
    private int _oldHealth;

    public DamageCommand(Player player, int damageAmount)
    {
        _player = player;
        _damageAmount = damageAmount;
    }

    public void Execute()
    {
        _oldHealth = _player.Health;
        _player.TakeDamage(_damageAmount);
        System.Console.WriteLine($"Took {_damageAmount} damage. Health: {_player.Health}");
    }

    public void Undo()
    {
        int healAmount = _player.Health - _oldHealth;
        _player.Heal(-healAmount);
        System.Console.WriteLine($"Undo: Health returned to {_player.Health}");
    }

    public string GetDescription()
    {
        return $"Damage -{_damageAmount} HP";
    }
}