public class Board
{
    public int Width { get; }
    public int Height { get; }

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public bool IsWall(int x, int y)
    {
        return x == 0 || x == Width - 1 ||
               y == 0 || y == Height - 1;
    }
    
    public bool IsWallCollision((int x, int y) pos)
    {
        return pos.x <= 0 || pos.x >= Width - 1 ||
               pos.y <= 0 || pos.y >= Height - 1;
    }

    public void Draw()
    {
        for (int x = 0; x < Width; x++)
        {
            Console.SetCursorPosition(x, 0);
            Console.Write("#");
            Console.SetCursorPosition(x, Height - 1);
            Console.Write("#");
        }

        for (int y = 0; y < Height; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write("#");
            Console.SetCursorPosition(Width - 1, y);
            Console.Write("#");
        }
    }
}