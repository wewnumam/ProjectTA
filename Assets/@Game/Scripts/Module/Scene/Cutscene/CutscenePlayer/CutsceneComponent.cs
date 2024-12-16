using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutsceneComponent : MonoBehaviour
    {
        [SerializeField] private List<CinemachineVirtualCamera> _cameras;

         public List<CinemachineVirtualCamera> Cameras => _cameras;

    }
}
