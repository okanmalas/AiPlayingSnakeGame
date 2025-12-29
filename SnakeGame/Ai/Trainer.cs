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

        public void Train(TrainingConfig cfg)
        {
            agent.Epsilon = cfg.StartEpsilon;
            agent.EpsilonMin = cfg.MinEpsilon;
            agent.EpsilonDecay = cfg.EpsilonDecay;
            if (agent.EpsilonDecay <= 0 || agent.EpsilonDecay >= 1)
                throw new Exception($"Invalid epsilon decay value: {agent.EpsilonDecay}");

            var env = new SnakeEnvironment(cfg.BoardWidth, cfg.BoardHeight);

            double bestRewardEver = double.NegativeInfinity;
            Dictionary<int, double[]>? bestQTable = null;

            double lastEpisodeReward = 0;

            for (int ep = 1; ep <= cfg.Episodes; ep++)
            {
                env.Reset();
                int state = env.GetState();
                double totalReward = 0;

                for (int step = 0; step < cfg.MaxSteps; step++)
                {
                    int action = agent.ChooseAction(state);
                    var (nextState, reward, done) = env.Step(action);

                    agent.Learn(state, action, reward, nextState);

                    state = nextState;
                    totalReward += reward;

                    if (done) break;
                }
                
                if (totalReward > bestRewardEver)
                {
                    bestRewardEver = totalReward;
                    bestQTable = agent.GetQTableCopy();
                }

                lastEpisodeReward = totalReward;

                agent.DecayEpsilon();

                if (ep % 100 == 0)
                {
                    Console.WriteLine(
                        $"Ep: {ep} | Reward: {totalReward:F2} | Best: {bestRewardEver:F2} | Epsilon: {agent.Epsilon:F3}");
                }
            }

            if (bestQTable != null && lastEpisodeReward < bestRewardEver)
            {
                agent.LoadQTable(bestQTable);
                agent.Save("snake_model.json");

                Console.WriteLine(
                    $"Best model saved (reward = {bestRewardEver:F2})");
            }
            else
            {
                agent.Save("snake_model.json");
                Console.WriteLine("Final model saved");
            }
        }
    }
}
