using ProjectTA.Module.LevelData;

namespace ProjectTA.Message
{
    public struct ActivateJoystickMessage
    {
        public bool IsJoystickActive { get; }

        public ActivateJoystickMessage(bool isJoystickActive) 
        { 
            IsJoystickActive = isJoystickActive;
        }
    }
}