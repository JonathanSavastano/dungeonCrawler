Console.WriteLine("Choose your class: Wizard, Warrior, Rogue");
string classChoice = Console.ReadLine()!.Trim().ToUpper();

Console.WriteLine("What is your name, adventurer?");
string characterName = (Console.ReadLine() ?? "Hero").Trim();

PlayerCharacter? player = classChoice switch
{
    "WIZARD" => new Wizard(characterName),
    "WARRIOR" => new Warrior(characterName),
    "ROGUE" => new Rogue(characterName),
    _ => null
};

if (player == null)
{
    Console.WriteLine("Invalid class choice. Please restart the game and choose a valid class.");
    return;
}

Console.WriteLine($"You have chosen the {classChoice} class!");
player.DrawArt();

Random rand = new Random();
const int attackCost = 2;
const int defendGain = 3;
int roomsExplored = 0;
int monstersSlain = 0;
bool alive = true;

while (alive)
{
    roomsExplored++;
    Console.WriteLine();
    Console.WriteLine($"--- Room {roomsExplored} ---");
    Room room = new Room(rand);
    room.Draw();
    Console.WriteLine($"Exits: {string.Join(", ", room.Exits)}");
    player.Stamina = player.MaxStamina;

    bool hasMonster = rand.NextDouble() < 0.45;
    bool fought = false;
    if (hasMonster)
    {
        MonsterCharacter monster = rand.Next(3) switch
        {
            0 => new Goblin("Gruk"),
            1 => new Orc("Ulag"),
            _ => new Ogre("Thokk")
        };
        monster.DrawArt();
        Console.WriteLine($"A {monster.Name} blocks your way!");

        bool fighting = true;

        if (fighting)
        {
            while (player.Health > 0 && monster.Health > 0)
            {
                if (monster.IsWindingUp)
                {
                    Console.WriteLine($"{monster.Name} is winding up a heavy attack!");
                }

                Console.WriteLine("Do you want to (A)ttack, (D)efend, or (F)lee?");
                string action = Console.ReadLine()!.Trim().ToUpper();

                bool attacking = action is "ATTACK" or "A";
                bool defending = action is "DEFEND" or "D";
                bool fleeing = action is "FLEE" or "F";

                if (!attacking && !defending && !fleeing)
                {
                    Console.WriteLine("Invalid choice. Please choose Attack, Defend, or Flee.");
                    continue;
                }

                if (attacking && player.Stamina < attackCost)
                {
                    Console.WriteLine("Too exhausted to attack! You are forced to defend.");
                    attacking = false;
                    defending = true;
                }

                if (fleeing)
                {
                    if (rand.NextDouble() < 0.5)
                    {
                        Console.WriteLine("You successfully flee the battle!");
                        fought = true;
                        break;
                    }
                    Console.WriteLine("You fail to flee! The monster gets a free hit!");
                    if (monster.IsWindingUp)
                    {
                        monster.performHeavyAttack(player, blocked: false);
                    }
                    else
                    {
                        monster.performAttack(player, rand);
                    }
                    monster.IsWindingUp = false;
                    PrintHealth(player, monster);
                    continue;
                }

                if (attacking)
                {
                    player.Stamina -= attackCost;
                    player.performAttack(monster, rand);
                    if (monster.Health <= 0)
                    {
                        break;
                    }

                    if (monster.IsWindingUp)
                    {
                        Console.WriteLine($"You interrupt {monster.Name}'s heavy attack!");
                        monster.IsWindingUp = false;
                    }
                    else
                    {
                        monster.performAttack(player, rand);
                        MaybeWindUp(monster, rand);
                    }
                }
                else
                {
                    player.Defending = true;
                    player.Stamina = Math.Min(player.MaxStamina, player.Stamina + defendGain);
                    Console.WriteLine($"{player.Name} raises their guard and defends! Stamina +{defendGain}.");

                    if (monster.IsWindingUp)
                    {
                        monster.performHeavyAttack(player, blocked: true);
                    }
                    else
                    {
                        monster.performAttack(player, rand);
                        MaybeWindUp(monster, rand);
                    }
                    player.Defending = false;
                }

                PrintHealth(player, monster);
            }

            if (player.Health <= 0)
            {
                Console.WriteLine($"You have been defeated by {monster.Name}!");
                alive = false;
            }
            else if (monster.Health <= 0)
            {
                monstersSlain++;
                Console.WriteLine($"You have defeated {monster.Name}!");
                fought = true;
                Console.WriteLine($"You gained {monster.XpValue} XP!");
                player.GainXP(monster.XpValue);
            }
        }
    }
    else
    {
        if (rand.NextDouble() < 0.35)
        {
            Item item = GetRandomItem(rand);
            Console.WriteLine($"You find a {item.Name}!");
            item.Use(player, rand);
        }
        else
        {
            Console.WriteLine("The room is empty, save for the dust in the air.");
        }
    }

    if (!alive)
    {
        break;
    }

    if (fought)
    {
        Console.WriteLine();
        room.Draw();
        Console.WriteLine($"Exits: {string.Join(", ", room.Exits)}");
    }

    while (true)
    {
        Console.WriteLine("Which direction do you want to go?");
        string choice = Console.ReadLine()!.Trim().ToUpper();

        string? direction = choice switch
        {
            "N" or "NORTH" => "North",
            "S" or "SOUTH" => "South",
            "W" or "WEST" => "West",
            "E" or "EAST" => "East",
            _ => null
        };

        if (direction != null && room.Exits.Contains(direction))
        {
            Console.WriteLine($"You head {direction}...");
            break;
        }

        Console.WriteLine("That is not a valid exit. Try again.");
    }
}

Console.WriteLine();
Console.WriteLine("--- Game Over ---");
Console.WriteLine($"Final level: {player.Level}");
Console.WriteLine($"Rooms explored: {roomsExplored}");
Console.WriteLine($"Monsters slain: {monstersSlain}");

void MaybeWindUp(MonsterCharacter m, Random r)
{
    if (r.NextDouble() < 0.5)
    {
        m.IsWindingUp = true;
        Console.WriteLine($"{m.WindUpMessage}");
    }
}

Item GetRandomItem(Random r)
{
    return r.Next(1) switch
    {
        _ => new Potion()
    };
}

void PrintHealth(PlayerCharacter p, MonsterCharacter m)
{
    Console.WriteLine($"Level {p.Level} | XP: {p.XP} | Your health: {Math.Max(0, p.Health)} | Stamina: {p.Stamina} | {m.Name} health: {Math.Max(0, m.Health)}");
}
