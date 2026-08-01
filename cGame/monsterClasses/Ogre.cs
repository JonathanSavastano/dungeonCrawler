class Ogre : MonsterCharacter
{
    public override string WindUpMessage => $"{Name} the Ogre lifts a massive club over its head...";

    public override int XpValue => 100;

    public Ogre(string name) : base(name, level: 1, health: 100, attack: 20)
    {
    }

    public override string[] Art => new string[]
    {
        @"    ________",
        @"   /        \",
        @"  |   o  o   |",
        @"  |    ^     |",
        @"  |  /___\   |",
        @"   \_______/",
        @"    /     \",
        @"   /|     |\",
        @"  / |     | \",
    };

    public override int performAttack(PlayerCharacter target, Random rand)
    {
        double missChance = target.Defending ? 0.5 : 0.2;
        if (rand.NextDouble() < missChance)
        {
            Console.WriteLine($"{Name} the Ogre lunges and misses!");
            return 0;
        }

        int damage = rand.Next(1, Attack + 1);
        target.Health -= damage;
        Console.WriteLine($"{Name} the Ogre attacks with {damage} damage!");
        return damage;
    }
}