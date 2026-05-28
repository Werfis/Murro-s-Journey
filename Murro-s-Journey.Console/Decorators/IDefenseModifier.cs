namespace Murro_s_Journey.Console.Decorators;

public interface IDefenseModifier
{
    int GetModifiedDamage(int incomingDamage);
    string GetDescription();
}