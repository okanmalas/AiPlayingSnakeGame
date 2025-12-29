using SnakeGame.Ai;

namespace SnakeGame
{
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

        public void Run(int latency, int width, int height)
        {
            ResetGame(width, height);
            gameOver = false;
            while (!gameOver)
            {
                Input();
                Update();
                Draw();
                Thread.Sleep(latency);
            }

            Console.SetCursorPosition(0, board.Height + 2);
            Console.WriteLine("Game Over!");
            Console.ReadKey();
        }

        public void RunWithAI(string modelPath, int latency, int width, int height)
        {
            ResetGame(width, height);
            var agent = new QLearningAgent();
            agent.Load(modelPath);   // epsilon = 0

            Console.CursorVisible = false;
            gameOver = false;

            int stepCount = 0;
            while (!gameOver)
            {
                int state = GetState();

                int action = agent.ChooseAction(state);
                snake.ApplyAction(action);

                Update();
                Draw();
                stepCount++;
                Thread.Sleep(latency);
            }

            Console.SetCursorPosition(0, board.Height + 2);
            Console.WriteLine("AI Game Over");
            Console.WriteLine($"Step Count: {stepCount}, Score: {snake.Length()}");
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

        private int GetState()
        {
            int state = 0;
            int bit = 0;

            if (snake.IsDangerAhead(board)) state |= 1 << bit;
            bit++;

            if (snake.IsDangerLeft(board)) state |= 1 << bit;
            bit++;

            if (snake.IsDangerRight(board)) state |= 1 << bit;
            bit++;

            if (food.Position.x < snake.Head.x) state |= 1 << bit;
            bit++;

            if (food.Position.x > snake.Head.x) state |= 1 << bit;
            bit++;

            if (food.Position.y < snake.Head.y) state |= 1 << bit;
            bit++;

            if (food.Position.y > snake.Head.y) state |= 1 << bit;
            bit++;

            if (snake.CurrentDirection == Snake.Direction.Up) state |= 1 << bit;
            bit++;

            if (snake.CurrentDirection == Snake.Direction.Down) state |= 1 << bit;
            bit++;

            if (snake.CurrentDirection == Snake.Direction.Left) state |= 1 << bit;
            bit++;

            if (snake.CurrentDirection == Snake.Direction.Right) state |= 1 << bit;
            bit++;

            return state;
        }
        private void ResetGame(int width, int height)
        {
            board = new Board(width, height);
            snake = new Snake(board.Width / 2, board.Height / 2);
            food = new Food(board.Width, board.Height);
            gameOver = false;
        }

    }
}
