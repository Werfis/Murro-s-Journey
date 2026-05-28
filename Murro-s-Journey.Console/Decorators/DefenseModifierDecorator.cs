namespace Murro_s_Journey.Console.Decorators;

public abstract class DefenseModifierDecorator : IDefenseModifier
{
    protected IDefenseModifier _inner;

    protected DefenseModifierDecorator(IDefenseModifier inner)
    {
        _inner = inner;
    }

    public abstract int GetModifiedDamage(int incomingDamage);
    public abstract string GetDescription();
}