using System;

namespace SnakeGame.Enviroment
{
    public class SnakeEnvironment
    {
        private Board board;
        private Snake snake;
        private Food food;

        private int previousDistance;

        public bool IsDone { get; private set; }

        public SnakeEnvironment(int width, int height)
        {
            board = new Board(width, height);
            Reset();
        }

        public void Reset()
        {
            snake = new Snake(board.Width / 2, board.Height / 2);
            food = new Food(board.Width, board.Height);
            IsDone = false;

            previousDistance = DistanceToFood();
        }

        public (int nextState, double reward, bool done) Step(int action)
        {
            if (IsDone)
                return (GetState(), 0, true);

            snake.ApplyAction(action);
            snake.Move();

            double reward = -0.001;

            if (board.IsWallCollision(snake.Head) || snake.IsSelfCollision())
            {
                reward = -20;
                IsDone = true;
                return (GetState(), reward, true);
            }
            
            if (snake.Head == food.Position)
            {
                reward += 20;
                snake.Grow();
                food.Spawn();
            }

            int currentDistance = DistanceToFood();

            if (currentDistance < previousDistance)
                reward += 0.5;
            else if (currentDistance > previousDistance)
                reward -= 0.5;

            previousDistance = currentDistance;

            return (GetState(), reward, IsDone);
        }

        private int DistanceToFood()
        {
            return Math.Abs(snake.Head.x - food.Position.x)
                 + Math.Abs(snake.Head.y - food.Position.y);
        }

        public int GetState()
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
    }
}
