namespace ProjectTA.Message
{
    public struct QuizScoreMessage
    {
        public float Score { get; }

        public QuizScoreMessage(float score) 
        { 
            Score = score;
        }
    }
}