public class Snake
{
    private List<(int x, int y)> body = new();
    private int dx = 1, dy = 0;

    public (int x, int y) Head => body[0];

    public Snake(int startX, int startY)
    {
        body.Add((startX, startY));
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction CurrentDirection
    {
        get
        {
            if (dx == 0 && dy == -1) return Direction.Up;
            if (dx == 0 && dy == 1)  return Direction.Down;
            if (dx == -1 && dy == 0) return Direction.Left;
            return Direction.Right;
        }
    }
    public void ChangeDirection(ConsoleKey key)
    {
        if ((key == ConsoleKey.UpArrow || key == ConsoleKey.W) && dy == 0)
        {
            dx = 0; dy = -1;
        }
        else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S) && dy == 0)
        {
            dx = 0; dy = 1;
        }
        else if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A) && dx == 0)
        {
            dx = -1; dy = 0;
        }
        else if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D) && dx == 0)
        {
            dx = 1; dy = 0;
        }
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
    public void ApplyAction(int action)
    {
        // 0 = straight
        // 1 = left
        // 2 = right

        if (action == 1) TurnLeft();
        else if (action == 2) TurnRight();
    }

    private void TurnLeft()
    {
        (dx, dy) = (-dy, dx);
    }

    private void TurnRight()
    {
        (dx, dy) = (dy, -dx);
    }

    public bool IsDangerAhead(Board board)
    {
        return IsDanger(board, dx, dy);
    }

    public bool IsDangerLeft(Board board)
    {
        return IsDanger(board, -dy, dx);
    }

    public bool IsDangerRight(Board board)
    {
        return IsDanger(board, dy, -dx);
    }

    private bool IsDanger(Board board, int nx, int ny)
    {
        var next = (Head.x + nx, Head.y + ny);
        return board.IsWallCollision(next) || body.Contains(next);
    }
    public int Length()
    {
        return body.Count;
    }
}
