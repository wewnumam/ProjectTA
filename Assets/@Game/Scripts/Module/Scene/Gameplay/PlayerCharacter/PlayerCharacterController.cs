using Agate.MVC.Base;
using DG.Tweening;
using ProjectTA.Message;
using ProjectTA.Module.CharacterData;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterController : ObjectController<PlayerCharacterController, PlayerCharacterView>
    {
        internal void OnGameOver(GameOverMessage message)
        {
        }

        internal void OnGameWin(GameWinMessage message)
        {
        }
    }
}