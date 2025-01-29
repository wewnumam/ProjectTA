using Agate.MVC.Base;
using ProjectTA.Module.GameConstants;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterModel : BaseModel
    {
        public PlayerConstants PlayerConstants { get; private set; } = null;
        public bool IsVibrationOn { get; private set; } = false;
        public bool IsJoystickActive { get; private set; }

        public void SetPlayerConstants(PlayerConstants playerConstants)
        {
            PlayerConstants = playerConstants;
        }

        public void SetIsVibrationOn(bool isVibrationOn)
        {
            IsVibrationOn = isVibrationOn;
        }

        public void SetIsJoystickActive(bool isVibrationOn)
        {
            IsVibrationOn = isVibrationOn;
        }
    }
}