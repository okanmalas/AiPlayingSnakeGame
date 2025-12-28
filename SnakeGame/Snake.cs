public class Snake
{
    private List<(int x, int y)> body = new();
    private int dx = 1, dy = 0;

    public (int x, int y) Head => body[0];

    public Snake(int startX, int startY)
    {
        body.Add((startX, startY));
    }

    public void ChangeDirection(ConsoleKey key)
    {
        if (key == ConsoleKey.UpArrow && dy == 0) { dx = 0; dy = -1; }
        if (key == ConsoleKey.DownArrow && dy == 0) { dx = 0; dy = 1; }
        if (key == ConsoleKey.LeftArrow && dx == 0) { dx = -1; dy = 0; }
        if (key == ConsoleKey.RightArrow && dx == 0) { dx = 1; dy = 0; }
        if (key == ConsoleKey.W && dy == 0) { dx = 0; dy = -1; }
        if (key == ConsoleKey.S && dy == 0) { dx = 0; dy = 1; }
        if (key == ConsoleKey.A && dx == 0) { dx = -1; dy = 0; }
        if (key == ConsoleKey.D && dx == 0) { dx = 1; dy = 0; }
    }

    public void Move()
    {
        var newHead = (Head.x + dx, Head.y + dy);
        body.Insert(0, newHead);
        body.RemoveAt(body.Count - 1);
    }

    public void Grow()
    {
        body.Add(body[^1]);
    }

    public bool IsSelfCollision()
    {
        return body.GetRange(1, body.Count - 1).Contains(Head);
    }

    public void Draw()
    {
        foreach (var part in body)
        {
            Console.SetCursorPosition(part.x, part.y);
            Console.Write("O");
        }
    }
}