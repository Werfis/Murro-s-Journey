namespace Murro_s_Journey.Console.Decorators;

public class NatureGuardianDecorator : DefenseModifierDecorator
{
    private Random _random;
    private int _blockChancePercent;

    public NatureGuardianDecorator(IDefenseModifier inner, int blockChancePercent = 30) : base(inner)
    {
        _random = new Random();
        _blockChancePercent = blockChancePercent;
    }

    public override int GetModifiedDamage(int incomingDamage)
    {
        int currentDamage = _inner.GetModifiedDamage(incomingDamage);
        
        if (_random.Next(100) < _blockChancePercent)
        {
            System.Console.WriteLine("Ironhead protects Murro! Damage blocked.");
            return 0;
        }
        
        return currentDamage;
    }

    public override string GetDescription()
    {
        return $"{_inner.GetDescription()} + Nature Guardian ({_blockChancePercent}% chance to block damage)";
    }
}