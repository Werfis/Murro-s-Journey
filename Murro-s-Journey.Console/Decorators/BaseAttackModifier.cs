namespace Murro_s_Journey.Console.Decorators;

public class BaseAttackModifier : IAttackModifier
{
    public int GetModifiedDamage(int baseDamage)
    {
        return baseDamage;
    }

    public string GetDescription()
    {
        return "No attack effects";
    }
}