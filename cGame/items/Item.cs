abstract class Item
{
    public string Name { get; }
    public string Description { get; }

    protected Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public abstract bool Use(PlayerCharacter player, Random rand);
}
