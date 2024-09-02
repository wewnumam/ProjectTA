using ProjectTA.Utility;

namespace ProjectTA.Message
{
    public struct GameResultHeartMessage
    {
        public int HeartAmount { get; }

        public GameResultHeartMessage(int heartAmount)
        {
            HeartAmount = heartAmount;
        }
    }
}