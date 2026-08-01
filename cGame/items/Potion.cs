class Potion : Item
{
    public Potion() : base("Health Potion", "A glowing red potion that restores health when drunk.")
    {
    }

    public override bool Use(PlayerCharacter player, Random rand)
    {
        if (player.Health >= player.MaxHealth)
        {
            Console.WriteLine($"You are already at full health, so you leave the {Name} behind.");
            return false;
        }

        int before = player.Health;
        player.Health = Math.Min(player.MaxHealth, player.Health + rand.Next(10, 30));
        Console.WriteLine($"You drink the {Name} and recover {player.Health - before} health! (Now at {player.Health}/{player.MaxHealth})");
        return true;
    }
}
