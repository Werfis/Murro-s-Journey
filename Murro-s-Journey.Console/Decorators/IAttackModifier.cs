namespace Murro_s_Journey.Console.Decorators;

public interface IAttackModifier
{
    int GetModifiedDamage(int baseDamage);
    string GetDescription();
}