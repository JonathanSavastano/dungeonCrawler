class Wizard : PlayerCharacter
{
    public Wizard(string name) : base(name, health: 80, level: 1, attack: 20)
    {
    }

    public override string[] Art => new string[]
    {
        @"      /\",
        @"     /  \",
        @"    |    |",
        @"    |    |",
        @"     \  /",
        @"     /==\",
        @"    |    |",
        @"    |    |",
        @"   /|    |\",
        @"  / |    | \",
    };

    public override int performAttack(MonsterCharacter target, Random rand)
    {
        if (rand.NextDouble() < 0.2)
        {
            Console.WriteLine($"{Name} tries to cast a spell but fumbles!");
            return 0;
        }

        int damage = rand.Next(1, Attack + 1);
        target.Health -= damage;
        Console.WriteLine($"{Name} casts a powerful spell for {damage} damage!");
        return damage;
    }
}