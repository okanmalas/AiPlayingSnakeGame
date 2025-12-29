namespace SnakeGame.Ai
{
    public class TrainingConfig
    {
        public int Episodes { get; set; }
        public int MaxSteps { get; set; }

        public double StartEpsilon { get; set; }
        public double MinEpsilon { get; set; }
        public double EpsilonDecay { get; set; }

        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
    }
}