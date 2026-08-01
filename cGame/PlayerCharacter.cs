class PlayerCharacter
{
    public const int MaxLevel = 10;

    public string Name { get; set; }
    public int Level { get; set; }
    public int XP { get; set; }
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
        XP = 0;
    }

    public int XpNeededForNextLevel => 100 + 25 * Level * (Level - 1);

    public void GainXP(int amount)
    {
        XP += amount;
        while (Level < MaxLevel && XP >= XpNeededForNextLevel)
        {
            XP -= XpNeededForNextLevel;
            Level++;
            Console.WriteLine($"Level up! You are now level {Level}!");
            Console.WriteLine("Choose a stat to increase:");
            Console.WriteLine("(H)ealth +10 | (S)tamina +2 | (A)ttack +5");

            string choice = Console.ReadLine()!.Trim().ToUpper();
            while (true)
            {
                if (choice is "H" or "HEALTH")
                {
                    MaxHealth += 10;
                    Console.WriteLine($"Your maximum health is now {MaxHealth}.");
                    break;
                }

                if (choice is "S" or "STAMINA")
                {
                    MaxStamina += 2;
                    Console.WriteLine($"Your maximum stamina is now {MaxStamina}.");
                    break;
                }

                if (choice is "A" or "ATTACK" or "ATK")
                {
                    Attack += 5;
                    Console.WriteLine($"Your attack is now {Attack}.");
                    break;
                }

                Console.WriteLine("Invalid choice. Please choose Health, Stamina, or Attack.");
                choice = Console.ReadLine()!.Trim().ToUpper();
            }

            Health = MaxHealth;
            Stamina = MaxStamina;
            Console.WriteLine("You are fully healed!");
        }
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