class Warrior : PlayerCharacter
{
    public Warrior(string name) : base(name, health: 120, level: 1, attack: 15)
    {
    }

    public override string[] Art => new string[]
    {
        @"    ___",
        @"   /   \",
        @"  | o o |",
        @"  |  -  |",
        @"   \_|_/",
        @"    | |",
        @"   /| |\",
        @"  | | | |",
        @"  | | | |",
        @"  |_|_|_|",
    };

    public override int performAttack(MonsterCharacter target, Random rand)
    {
        if (rand.NextDouble() < 0.2)
        {
            Console.WriteLine($"{Name} swings his mighty sword but misses!");
            return 0;
        }

        int damage = rand.Next(1, Attack + 1);
        target.Health -= damage;
        Console.WriteLine($"{Name} swings a mighty sword for {damage} damage!");
        return damage;
    }
}