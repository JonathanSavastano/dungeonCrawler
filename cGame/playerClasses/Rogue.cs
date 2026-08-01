class Rogue : PlayerCharacter 
{
    public Rogue(string name) : base(name, health: 100, level: 1, attack: 20)
    {
    }

    public override string[] Art => new string[]
    {
        @"    _____",
        @"   /  _  \",
        @"  |  (_)  |",
        @"  |   -   |",
        @"   \  |  /",
        @"    \ | /",
        @"     \|/",
        @"     / \",
        @"    /| |\",
        @"   / | | \",
    };

    public override int performAttack(MonsterCharacter target, Random rand)
    {
        if (rand.NextDouble() < 0.2)
        {
            Console.WriteLine($"{Name} strikes from the shadows but misses!");
            return 0;
        }

        int damage = rand.Next(1, Attack + 1);
        target.Health -= damage;
        Console.WriteLine($"{Name} strikes from the shadows for {damage} damage!");
        return damage;
    }
}