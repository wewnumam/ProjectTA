namespace ProjectTA.Message
{
    public struct UpdateKillCountMessage
    {
        public int KillCount { get; }
        public bool IsIncrease { get; }
        
        public UpdateKillCountMessage(int killCount, bool isIncrease)
        {
            KillCount = killCount;
            IsIncrease = isIncrease;
        }
    }
}