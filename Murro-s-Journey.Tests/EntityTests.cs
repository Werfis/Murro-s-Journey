using Murro_s_Journey.Console.Entities;

namespace Murro_s_Journey.Tests;

public class EntityTests
{
    private class TestEntity : Entity
    {
        public TestEntity(string name, int maxHealth, int startX, int startY) 
            : base(name, maxHealth, startX, startY)
        {
        }

        public override void Update() { }
        public override void Draw() { }
    }

    [Fact]
    public void TakeDamage_NormalDamage_HealthDecreasesCorrectly()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        int damage = 30;
        int expectedHealth = 70;

        int actualDamage = entity.TakeDamage(damage);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(damage, actualDamage);
        Assert.True(entity.IsAlive());
    }

    [Fact]
    public void TakeDamage_DamageGreaterThanHealth_HealthBecomesZero()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        int damage = 150;
        int expectedHealth = 0;

        int actualDamage = entity.TakeDamage(damage);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(100, actualDamage);
        Assert.False(entity.IsAlive());
    }

    [Fact]
    public void TakeDamage_NegativeDamage_HealthUnchanged()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        int damage = -50;
        int expectedHealth = 100;

        int actualDamage = entity.TakeDamage(damage);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(0, actualDamage);
        Assert.True(entity.IsAlive());
    }

    [Fact]
    public void TakeDamage_ZeroDamage_HealthUnchanged()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        int damage = 0;
        int expectedHealth = 100;

        int actualDamage = entity.TakeDamage(damage);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(0, actualDamage);
        Assert.True(entity.IsAlive());
    }

    [Fact]
    public void TakeDamage_AlreadyDead_HealthRemainsZero()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        entity.TakeDamage(150);
        int damage = 20;
        int expectedHealth = 0;

        int actualDamage = entity.TakeDamage(damage);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(0, actualDamage);
        Assert.False(entity.IsAlive());
    }

    [Fact]
    public void Heal_NormalHeal_HealthIncreasesCorrectly()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        entity.TakeDamage(50);
        int healAmount = 30;
        int expectedHealth = 80;

        int actualHeal = entity.Heal(healAmount);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(healAmount, actualHeal);
        Assert.True(entity.IsAlive());
    }

    [Fact]
    public void Heal_OverHeal_HealthCapsAtMax()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        entity.TakeDamage(50);
        int healAmount = 70;
        int expectedHealth = 100;

        int actualHeal = entity.Heal(healAmount);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(50, actualHeal);
        Assert.True(entity.IsAlive());
    }

    [Fact]
    public void Heal_NegativeAmount_HealthUnchanged()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        entity.TakeDamage(50);
        int healAmount = -30;
        int expectedHealth = 50;

        int actualHeal = entity.Heal(healAmount);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(0, actualHeal);
        Assert.True(entity.IsAlive());
    }

    [Fact]
    public void Heal_ZeroAmount_HealthUnchanged()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        entity.TakeDamage(50);
        int healAmount = 0;
        int expectedHealth = 50;

        int actualHeal = entity.Heal(healAmount);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(0, actualHeal);
        Assert.True(entity.IsAlive());
    }

    [Fact]
    public void Heal_DeadCharacter_ShouldNotHeal()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        entity.TakeDamage(150);
        int healAmount = 50;
        int expectedHealth = 0;

        int actualHeal = entity.Heal(healAmount);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(0, actualHeal);
        Assert.False(entity.IsAlive());
    }

    [Fact]
    public void Heal_AtFullHealth_NoChange()
    {
        var entity = new TestEntity("Test", 100, 0, 0);
        int healAmount = 50;
        int expectedHealth = 100;

        int actualHeal = entity.Heal(healAmount);

        Assert.Equal(expectedHealth, entity.Health);
        Assert.Equal(0, actualHeal);
        Assert.True(entity.IsAlive());
    }
}