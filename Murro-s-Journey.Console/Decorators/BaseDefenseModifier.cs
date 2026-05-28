namespace Murro_s_Journey.Console.Decorators;

public class BaseDefenseModifier : IDefenseModifier
{
    public int GetModifiedDamage(int incomingDamage)
    {
        return incomingDamage;
    }

    public string GetDescription()
    {
        return "No defense effects";
    }
}