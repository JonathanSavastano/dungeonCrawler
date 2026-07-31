class Room
{
    private const int Width = 15;
    private const int Height = 9;
    private readonly char[,] grid = new char[Height, Width];
    private readonly Random rand;
    public List<string> Exits { get; } = new List<string>();

    public Room(Random rand)
    {
        this.rand = rand;
        FillGrid();
        ChooseExits();
    }

    private void FillGrid()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                bool border = y == 0 || y == Height - 1 || x == 0 || x == Width - 1;
                if (border)
                {
                    bool corner = (y == 0 || y == Height - 1) && (x == 0 || x == Width - 1);
                    if (corner)
                    {
                        grid[y, x] = '+';
                    }
                    else if (y == 0 || y == Height - 1)
                    {
                        grid[y, x] = '-';
                    }
                    else
                    {
                        grid[y, x] = '|';
                    }
                }
                else
                {
                    double d = rand.NextDouble();
                    if (d < 0.1)
                    {
                        grid[y, x] = '*';
                    }
                    else if (d < 0.2)
                    {
                        grid[y, x] = '.';
                    }
                    else
                    {
                        grid[y, x] = ' ';
                    }
                }
            }
        }
    }

    private void ChooseExits()
    {
        List<string> directions = new List<string> { "North", "East", "South", "West" };
        for (int i = directions.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (directions[i], directions[j]) = (directions[j], directions[i]);
        }

        int count = rand.Next(1, 5);
        for (int i = 0; i < count; i++)
        {
            Exits.Add(directions[i]);
            CarveExit(directions[i]);
        }
    }

    private void CarveExit(string direction)
    {
        int midX = Width / 2;
        int midY = Height / 2;
        switch (direction)
        {
            case "North":
                for (int x = midX - 1; x <= midX + 1; x++)
                {
                    grid[0, x] = ' ';
                }
                grid[1, midX] = ' ';
                break;
            case "South":
                for (int x = midX - 1; x <= midX + 1; x++)
                {
                    grid[Height - 1, x] = ' ';
                }
                grid[Height - 2, midX] = ' ';
                break;
            case "West":
                for (int y = midY - 1; y <= midY + 1; y++)
                {
                    grid[y, 0] = ' ';
                }
                grid[midY, 1] = ' ';
                break;
            case "East":
                for (int y = midY - 1; y <= midY + 1; y++)
                {
                    grid[y, Width - 1] = ' ';
                }
                grid[midY, Width - 2] = ' ';
                break;
        }
    }

    public void Draw()
    {
        grid[Height / 2, Width / 2] = '@';
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.Write(grid[y, x]);
            }
            Console.WriteLine();
        }
    }
}
