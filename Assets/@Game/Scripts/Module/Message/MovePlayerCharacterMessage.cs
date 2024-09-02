namespace ProjectTA.Message
{
    public struct MovePlayerCharacterMessage 
    {
        public float MoveAmount { get; }

        public MovePlayerCharacterMessage(float moveAmount = 1)
        {
            MoveAmount = moveAmount;
        }
    }
}