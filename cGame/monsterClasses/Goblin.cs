class Goblin : MonsterCharacter
{
    public override string WindUpMessage => $"{Name} the Goblin crouches low, ready to pounce...";

    public Goblin(string name) : base(name, level: 1, health: 50, attack: 10)
    {
    }

    public override string[] Art => new string[]
    {
        @"    /\  /\",
        @"   /  \/  \",
        @"  |  o  o  |",
        @"   \  __  /",
        @"    \|  |/",
        @"     |  |",
        @"    /|  |\",
        @"   / |  | \",
    };

    public override int performAttack(PlayerCharacter target, Random rand)
    {
        double missChance = target.Defending ? 0.5 : 0.2;
        if (rand.NextDouble() < missChance)
        {
            Console.WriteLine($"{Name} the Goblin swipes and misses!");
            return 0;
        }

        int damage = rand.Next(1, Attack + 1);
        target.Health -= damage;
        Console.WriteLine($"{Name} the Goblin attacks with {damage} damage!");
        return damage;
    }
}