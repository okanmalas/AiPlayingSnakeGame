namespace SnakeGame.Ai
{
    public class TrainingLog
    {
        public int Episode { get; set; }
        public int Steps { get; set; }
        public double Reward { get; set; }
        public double Epsilon { get; set; }
        public DateTime Time { get; set; }
    }
}