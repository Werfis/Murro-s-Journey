namespace Murro_s_Journey.Console.Decorators;

public abstract class AttackModifierDecorator : IAttackModifier
{
    protected IAttackModifier _inner;

    protected AttackModifierDecorator(IAttackModifier inner)
    {
        _inner = inner;
    }

    public abstract int GetModifiedDamage(int baseDamage);
    public abstract string GetDescription();
}