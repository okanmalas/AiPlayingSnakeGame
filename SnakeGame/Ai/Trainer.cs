using System;
using SnakeGame.Enviroment;

namespace SnakeGame.Ai
{
    public class Trainer
    {
        private QLearningAgent agent;

        public Trainer(QLearningAgent? existingAgent = null)
        {
            agent = existingAgent ?? new QLearningAgent();
        }

        public void Train(int episodes, int maxSteps)
        {
            var env = new SnakeEnvironment(40, 20);
            var logger = new TrainingLogger();

            for (int ep = 1; ep <= episodes; ep++)
            {
                env.Reset();
                int state = env.GetState();
                double totalReward = 0;

                for (int step = 0; step < maxSteps; step++)
                {
                    int action = agent.ChooseAction(state);
                    var (nextState, reward, done) = env.Step(action);

                    agent.Learn(state, action, reward, nextState);

                    state = nextState;
                    totalReward += reward;

                    if (done) break;
                }

                agent.DecayEpsilon();

                if (ep % 100 == 0)
                {
                    Console.WriteLine(
                        $"Ep: {ep} | Reward: {totalReward:F2} | Epsilon: {agent.Epsilon:F2}");
                }
            }

            agent.Save("snake_model.json");
            Console.WriteLine("Model saved.");
        }
    }
}