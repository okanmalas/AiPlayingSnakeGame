namespace SnakeGame.Ai
{
    public static class TrainingPresets
    {
        public static TrainingConfig Survival => new()
        {
            Episodes = 3000,
            MaxSteps = 200,
            StartEpsilon = 1.0,
            MinEpsilon = 0.1,
            EpsilonDecay = 0.995,
            BoardWidth = 20,
            BoardHeight = 10
        };

        public static TrainingConfig FoodChasing => new()
        {
            Episodes = 5000,
            MaxSteps = 400,
            StartEpsilon = 0.8,
            MinEpsilon = 0.05,
            EpsilonDecay = 0.997,
            BoardWidth = 30,
            BoardHeight = 15
        };

        public static TrainingConfig Optimization => new()
        {
            Episodes = 3000,
            MaxSteps = 800,
            StartEpsilon = 0.5,
            MinEpsilon = 0.02,
            EpsilonDecay = 0.999,
            BoardWidth = 40,
            BoardHeight = 20
        };
    }
}