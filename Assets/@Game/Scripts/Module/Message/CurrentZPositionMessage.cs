namespace ProjectTA.Message
{
    public struct CurrentZPositionMessage
    {
        public float ZPosition { get; }

        public CurrentZPositionMessage(float zPosition)
        {
            ZPosition = zPosition;
        }
    }
}