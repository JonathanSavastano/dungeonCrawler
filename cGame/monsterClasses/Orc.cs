class Orc : MonsterCharacter
{
    public override string WindUpMessage => $"{Name} the Orc raises a jagged axe high above his head...";

    public override int XpValue => 70;

    public Orc(string name) : base(name, level: 1, health: 75, attack: 15)
    {
    }

    public override string[] Art => new string[]
    {
        @"     _______",
        @"    /   |   \",
        @"   |  o   o  |",
        @"   |    ^    |",
        @"   | |_| |_| |",
        @"    \__   __/",
        @"      / | \",
        @"     |  |  |",
        @"    /   |   \",
        @"   /|   |   |\",
    };

    public override int performAttack(PlayerCharacter target, Random rand)
    {
        double missChance = target.Defending ? 0.5 : 0.2;
        if (rand.NextDouble() < missChance)
        {
            Console.WriteLine($"{Name} the Orc swings and misses!");
            return 0;
        }

        int damage = rand.Next(1, Attack + 1);
        target.Health -= damage;
        Console.WriteLine($"{Name} the Orc attacks with {damage} damage!");
        return damage;
    }
}