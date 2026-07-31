class MonsterCharacter
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool IsWindingUp { get; set; }

    public MonsterCharacter(string name, int level, int health, int attack)
    {
        Name = name;
        Level = level;
        Health = health;
        Attack = attack;
    }

    public virtual string WindUpMessage => $"{Name} winds up a heavy attack...";

    public virtual string[] Art => new string[]
    {
        @"    ___",
        @"   /   \",
        @"  | o o |",
        @"  |  -  |",
        @"   \_|_/",
        @"    | |",
        @"   /| |\",
        @"  / | | \",
    };

    public void DrawArt()
    {
        foreach (string line in Art)
        {
            Console.WriteLine(line);
        }
    }

    public virtual int performAttack (PlayerCharacter target, Random rand)
    {
        double missChance = target.Defending ? 0.5 : 0.2;
        if (rand.NextDouble() < missChance)
        {
            Console.WriteLine($"{Name} attacks and misses!");
            return 0;
        }

        int damage = rand.Next(1, Attack + 1);
        target.Health -= damage;
        Console.WriteLine($"{Name} attacks with {damage} damage!");
        return damage;
    }

    public virtual int performHeavyAttack(PlayerCharacter target, bool blocked)
    {
        int damage = (int)(Attack * 1.5);
        if (blocked)
        {
            damage /= 2;
        }
        target.Health -= damage;
        Console.WriteLine(blocked
            ? $"{Name} smashes into your guard, dealing {damage} damage!"
            : $"{Name} lands a devastating blow for {damage} damage!");
        return damage;
    }
}