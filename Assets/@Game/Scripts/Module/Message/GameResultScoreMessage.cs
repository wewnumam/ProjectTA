using ProjectTA.Utility;

namespace ProjectTA.Message
{
    public struct GameResultScoreMessage
    {
        public int Score { get; }

        public GameResultScoreMessage(int score)
        {
            Score = score;
        }
    }
}