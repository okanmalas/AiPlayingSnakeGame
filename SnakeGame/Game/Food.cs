public class Food
{
    private Random rand = new Random();
    private int width, height;

    public (int x, int y) Position { get; private set; }

    public Food(int width, int height)
    {
        this.width = width;
        this.height = height;
        Spawn();
    }

    public void Spawn()
    {
        Position = (
            rand.Next(1, width - 1),
            rand.Next(1, height - 1)
        );
    }

    public void Draw()
    {
        Console.SetCursorPosition(Position.x, Position.y);
        Console.Write("@");
    }
}