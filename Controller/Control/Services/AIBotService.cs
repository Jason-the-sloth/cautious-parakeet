using Control.Models;
using Microsoft.ML.OnnxRuntime;

namespace Control.Services
{
    public class AIBotService : IAIBotService
    {
        public void GetCommands(BotInput botInput)
        {
            {
                const string modelPath = "./testBot.onnx";
                using InferenceSession session = new InferenceSession(modelPath);

                var player_x = botInput.Player.Position.X;
                var player_y = botInput.Player.Position.Y;
                var player_velocity = botInput.Player.Velocity.SqrMagnitude;
                var player_rotation = botInput.Player.Rotation;
                var player_health = botInput.Player.Health;
                var player_score = botInput.Player.Score;

                float[] inputData = { player_x, player_y, player_velocity, player_rotation, player_health, player_score };
                long[] inputShape = { 1, 6 };

                using var inputOrtValue = OrtValue.CreateTensorValueFromMemory(inputData, inputShape);

                // Create input data for session. Request all outputs in this case.
                var inputs = new Dictionary<string, OrtValue>
                {
                    { "l_x_", inputOrtValue }
                };

                using var runOptions = new RunOptions();

                // session1 inference
                using (var outputs = session.Run(runOptions, inputs, session.OutputNames))
                {
                    // get intermediate value
                    var outputToFeed = outputs.First();

                    //var x_move = torch.tensor(labels["Move_X"], torch.float32);
                    //var y_move = torch.tensor(labels['Move_Y'], torch.float32);
                    //var rotate = torch.tensor(labels['Rotate'], torch.float32);
                    //var shoot = torch.tensor(labels['Shoot'], torch.float32);

                    //// modify the name of the ONNX value
                    //// create input list for session2
                    //var inputs2 = new Dictionary<string, OrtValue>
                    //{
                    //    { "inputNameForModelB", outputToFeed }
                    //};

                    //// session2 inference
                    //using (var results = session2.Run(runOptions, inputs2, session2.OutputNames))
                    //{
                    //    // manipulate the results
                    //}
                }
            }
        }
    }
}
