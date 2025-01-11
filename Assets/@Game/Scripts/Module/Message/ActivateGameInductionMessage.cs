namespace ProjectTA.Message
{
    public struct ActivateGameInductionMessage
    {
        public bool IsGameInductionActive { get; }

        public ActivateGameInductionMessage(bool isGameInductionActive) 
        { 
            IsGameInductionActive = isGameInductionActive;
        }
    }
}