using Agate.MVC.Base;

namespace ProjectTA.Module.GameInduction
{
    public class GameInductionModel : BaseModel
    {
        public bool IsGameInductionActive { get; private set; }

        public void SetIsGameInductionActive(bool isGameInductionActive)
        {
            IsGameInductionActive = isGameInductionActive;
        }
    }
}