using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.SpatialDirection
{
    public class SpatialDirectionView : BaseView
    {
        [SerializeField] private Transform anchor;

        public Transform Target { get; set; }

        [field: SerializeField]
        public UnityEvent<bool> IsActive { get; set; }

        private void Update()
        {
            if (Target != null)
            {
                anchor.LookAt(Target);
            }
        }
    }
}