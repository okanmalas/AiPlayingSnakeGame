using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SnakeGame.Ai
{
    public class TrainingLogger
    {
        private readonly string txtPath = "training_log.txt";
        private readonly string jsonPath = "training_log.json";

        private List<TrainingLog> jsonBuffer = new();

        public TrainingLogger()
        {
            File.WriteAllText(txtPath, "Episode | Steps | Reward | Epsilon\n");
            File.WriteAllText(jsonPath, "");
        }

        public void Log(TrainingLog log)
        {
            // TXT
            File.AppendAllText(
                txtPath,
                $"{log.Episode} | {log.Steps} | {log.Reward:F2} | {log.Epsilon:F2}\n"
            );

            // JSON buffer
            jsonBuffer.Add(log);
        }

        public void FlushJson()
        {
            var json = JsonSerializer.Serialize(
                jsonBuffer,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(jsonPath, json);
        }
    }
}