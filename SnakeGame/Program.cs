using System.Globalization;
using SnakeGame;
using SnakeGame.Ai;

var game = new Game();
char key;

while (true)
{
    Console.Clear();
    Console.WriteLine("1 - Train AI");
    Console.WriteLine("2 - Watch AI Play");
    Console.WriteLine("3 - Play Snake");
    Console.WriteLine("0 - Exit");
    key = Console.ReadKey(true).KeyChar;
    switch (key)
    {
        case '1': // Train AI
            Console.Clear();
            Console.WriteLine("1 - Train from scratch");
            Console.WriteLine("2 - Continue existing model");

            char mode = Console.ReadKey(true).KeyChar;

            QLearningAgent agent;

            if (mode == '1')
            {
                agent = new QLearningAgent();
            }
            else
            {
                agent = new QLearningAgent();
                agent.Load("snake_model.json");

                Console.WriteLine("Reset epsilon? (y/n)");
                if (Console.ReadKey(true).KeyChar == 'y')
                    agent.Epsilon = 1.0;
            }

            Console.Clear();
            Console.WriteLine("Select training preset:");
            Console.WriteLine("1 - Survival");
            Console.WriteLine("2 - Food chasing");
            Console.WriteLine("3 - Optimization");
            Console.WriteLine("4 - Custom");

            char preset = Console.ReadKey(true).KeyChar;
            TrainingConfig cfg;

            if (preset == '1') cfg = TrainingPresets.Survival;
            else if (preset == '2') cfg = TrainingPresets.FoodChasing;
            else if (preset == '3') cfg = TrainingPresets.Optimization;
            else
            {
                cfg = new TrainingConfig();

                Console.WriteLine("Episodes:");
                cfg.Episodes = int.Parse(Console.ReadLine());

                Console.WriteLine("Max steps:");
                cfg.MaxSteps = int.Parse(Console.ReadLine());

                Console.WriteLine("Board width:");
                cfg.BoardWidth = int.Parse(Console.ReadLine());

                Console.WriteLine("Board height:");
                cfg.BoardHeight = int.Parse(Console.ReadLine());

                Console.WriteLine("Start epsilon:");
                cfg.StartEpsilon = double.Parse(
                    Console.ReadLine(),
                    CultureInfo.InvariantCulture);

                Console.WriteLine("Min epsilon:");
                cfg.MinEpsilon = double.Parse(
                    Console.ReadLine(),
                    CultureInfo.InvariantCulture);

                Console.WriteLine("Epsilon decay:");
                cfg.EpsilonDecay = double.Parse(
                    Console.ReadLine(),
                    CultureInfo.InvariantCulture);
            }

            new Trainer(agent).Train(cfg);
            break;


        case '2': 
            Console.Clear();
            Console.WriteLine("enter the frame latency (miliseconds) >");
            int latency = int.Parse(Console.ReadLine());
            Console.WriteLine("enter map width");
            int mapWidth = int.Parse(Console.ReadLine());
            Console.WriteLine("enter map height");
            int mapHeight = int.Parse(Console.ReadLine());
            game.RunWithAI("snake_model.json",latency, mapWidth, mapHeight);
            break;
        case '3':
            Console.Clear();
            Console.WriteLine("enter the frame latency (miliseconds) >");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("enter map width");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("enter map height");
            int c = int.Parse(Console.ReadLine());
            game.Run(a, b, c);
            break;
        case '0':
            Environment.Exit(0);
            break;
    }
}