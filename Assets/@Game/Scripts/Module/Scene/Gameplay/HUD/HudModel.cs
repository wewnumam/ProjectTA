using Agate.MVC.Base;
using UnityEngine;

namespace ProjectTA.Module.HUD
{
    public class HudModel : BaseModel
    {
        public Sprite GateIcon { get; private set; } = null;

        public void SetGateIcon(Sprite gateIcon)
        {
            GateIcon = gateIcon;
        }
    }
}