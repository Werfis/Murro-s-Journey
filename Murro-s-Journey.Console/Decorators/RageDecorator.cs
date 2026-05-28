namespace Murro_s_Journey.Console.Decorators;

public class RageDecorator : AttackModifierDecorator
{
    private float _multiplier;

    public RageDecorator(IAttackModifier inner, float multiplier = 1.5f) : base(inner)
    {
        _multiplier = multiplier;
    }

    public override int GetModifiedDamage(int baseDamage)
    {
        int currentDamage = _inner.GetModifiedDamage(baseDamage);
        return (int)(currentDamage * _multiplier);
    }

    public override string GetDescription()
    {
        return $"{_inner.GetDescription()} + Rage (damage x{_multiplier})";
    }
}