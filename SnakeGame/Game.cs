namespace SnakeGame;

public class Game
{
    private Board board;
    private Snake snake;
    private Food food;
    private bool gameOver = false;

    public Game()
    {
        Console.CursorVisible = false;
        board = new Board(40, 20);
        snake = new Snake(board.Width / 2, board.Height / 2);
        food = new Food(board.Width, board.Height);
    }

    public void Run()
    {
        while (!gameOver)
        {
            Input();
            Update();
            Draw();
            Thread.Sleep(120);
        }

        Console.SetCursorPosition(0, board.Height + 2);
        Console.WriteLine("Game Over!");
        Console.ReadKey();
    }

    private void Input()
    {
        if (!Console.KeyAvailable) return;

        var key = Console.ReadKey(true).Key;
        snake.ChangeDirection(key);
    }

    private void Update()
    {
        snake.Move();

        if (board.IsWallCollision(snake.Head))
            gameOver = true;

        if (snake.IsSelfCollision())
            gameOver = true;

        if (snake.Head == food.Position)
        {
            snake.Grow();
            food.Spawn();
        }
    }

    private void Draw()
    {
        Console.Clear();
        board.Draw();
        food.Draw();
        snake.Draw();
    }
}