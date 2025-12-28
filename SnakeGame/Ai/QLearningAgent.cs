using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SnakeGame.Ai
{
    public class QLearningAgent
    {
        private Dictionary<int, double[]> qTable = new();
        private Random rand = new Random();

        public double Alpha = 0.1;
        public double Gamma = 0.95;
        public double Epsilon = 1.0;
        public double EpsilonMin = 0.05;
        public double EpsilonDecay = 0.999;

        public int ChooseAction(int state)
        {
            if (!qTable.ContainsKey(state))
                qTable[state] = new double[3];

            if (rand.NextDouble() < Epsilon)
                return rand.Next(3);

            return ArgMax(qTable[state]);
        }

        public void Learn(int state, int action, double reward, int nextState)
        {
            if (!qTable.ContainsKey(state))
                qTable[state] = new double[3];

            if (!qTable.ContainsKey(nextState))
                qTable[nextState] = new double[3];

            double predict = qTable[state][action];
            double target = reward + Gamma * Max(qTable[nextState]);

            qTable[state][action] += Alpha * (target - predict);
        }

        public void DecayEpsilon()
        {
            if (Epsilon > EpsilonMin)
                Epsilon *= EpsilonDecay;
        }

        private int ArgMax(double[] arr)
        {
            int best = 0;
            for (int i = 1; i < arr.Length; i++)
                if (arr[i] > arr[best]) best = i;
            return best;
        }

        private double Max(double[] arr)
        {
            double max = arr[0];
            foreach (var v in arr)
                if (v > max) max = v;
            return max;
        }
        public void Save(string path)
        {
            var json = JsonSerializer.Serialize(qTable,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(path, json);
        }

        public void Load(string path)
        {
            var json = File.ReadAllText(path);
            qTable = JsonSerializer.Deserialize<Dictionary<int, double[]>>(json);

            Epsilon = 0.0;
        }
    }
}