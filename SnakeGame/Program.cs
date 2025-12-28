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
        case '1':
            Console.Clear();
            Console.WriteLine("1 - Train from scratch");
            Console.WriteLine("2 - Continue training existing model");
            char sub = Console.ReadKey(true).KeyChar;
            Console.WriteLine("Enter episode count:");
            int ep = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter max steps per episode:");
            int max = int.Parse(Console.ReadLine());

            if (sub == '1')
            {
                var trainer = new Trainer();
                trainer.Train(ep, max);
            }
            else if (sub == '2')
            {
                var agent = new QLearningAgent();
                agent.Load("snake_model.json");
                agent.Epsilon = 0.5;
                var trainer = new Trainer(agent);
                trainer.Train(ep, max);
            }
            break;

        case '2': 
            Console.Clear();
            Console.WriteLine("enter the frame latency (miliseconds) >");
            game.RunWithAI("snake_model.json",int.Parse(Console.ReadLine()));
            break;
        case '3':
            game.Run();
            break;
        case '0':
            Environment.Exit(0);
            break;
    }
}