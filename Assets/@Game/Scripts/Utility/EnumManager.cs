namespace ProjectTA.Utility
{
    public class EnumManager
    {
        public enum GameState
        {
            PreGame,
            Playing,
            Pause,
            GameOver,
            GameWin,
            Dialogue
        }

        public enum CollectibleType
        {
            Puzzle,
            HiddenObject
        }

        public enum QuestType
        {
            Kill,
            Collectible,
            Puzzle,
            HiddenObject,
            LevelPlayed,
            GameWin,
            MinutesPlayed,
            QuizScore
        }
    }
}