using Cinemachine;
using UnityEngine;

namespace ProjectTA.Utility
{
    public class CinemachineOrbit : MonoBehaviour
    {
        public CinemachineVirtualCamera virtualCamera;
        public float rotationSpeed = 30f; // Speed of rotation in degrees per second

        private CinemachineOrbitalTransposer orbitalTransposer;

        void Start()
        {
            // Get the Orbital Transposer component
            if (virtualCamera != null)
            {
                orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            }
        }

        void Update()
        {
            if (orbitalTransposer != null)
            {
                // Update the heading bias to create the orbit effect
                orbitalTransposer.m_Heading.m_Bias += rotationSpeed * Time.deltaTime;

                // Keep the heading bias within a 0-360 range
                if (orbitalTransposer.m_Heading.m_Bias >= 360f)
                    orbitalTransposer.m_Heading.m_Bias -= 360f;
            }
        }
    }
}