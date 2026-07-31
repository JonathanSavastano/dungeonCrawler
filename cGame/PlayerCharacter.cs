class PlayerCharacter
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Attack { get; set; }
    public bool Defending { get; set; }
    public int MaxStamina { get; set; }
    public int Stamina { get; set; }

    public PlayerCharacter(string name, int level, int health, int attack)
    {
        Name = name;
        Level = level;
        Health = health;
        MaxHealth = health;
        Attack = attack;
        MaxStamina = 10;
        Stamina = 10;
    }

    public virtual string[] Art => new string[]
    {
        @"   ____",
        @"  /    \",
        @" |  o o |",
        @" |   -  |",
        @"  \_|_/",
        @"    |",
        @"   /|\",
        @"  / | \",
    };

    public void DrawArt()
    {
        foreach (string line in Art)
        {
            Console.WriteLine(line);
        }
    }

    public virtual int performAttack (MonsterCharacter target, Random rand)
    {
        if (rand.NextDouble() < 0.2)
        {
            Console.WriteLine($"{Name} attacks and misses!");
            return 0;
        }

        int damage = rand.Next(1, Attack + 1);
        target.Health -= damage;
        Console.WriteLine($"{Name} attacks with {damage} damage!");
        return damage;
    }
}