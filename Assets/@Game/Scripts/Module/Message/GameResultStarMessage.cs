using ProjectTA.Utility;

namespace ProjectTA.Message
{
    public struct GameResultStarMessage
    {
        public int StarAmount { get; }

        public GameResultStarMessage(int starAmount)
        {
            StarAmount = starAmount;
        }
    }
}