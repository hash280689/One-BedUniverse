using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HPP.CameraControls
{
    /// <summary>
    /// Camera Manager for moving camera 
    /// </summary>
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera m_MainVirtualCam;
        [SerializeField] private CinemachineVirtualCamera m_ZoomedInVirtualCam;

        private float m_DollyPosition = 0;
        [SerializeField] private float m_Increment = 0.5f;

        public static CameraManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            m_ZoomedInVirtualCam.Priority = 1;
            UseMainCam();
        }

        public void UseMainCam()
        {
            m_MainVirtualCam.Priority = 2;
        }

        public void UseZoomedInCam()
        {
            m_MainVirtualCam.Priority = 0;
        }

        public void MoveCamClockwise()
        {
            m_DollyPosition += m_Increment;
            UpdateCamPositions();
        }
        public void MoveCamAntiClockwise()
        {
            m_DollyPosition -= m_Increment;
            UpdateCamPositions();
        }

        private void UpdateCamPositions()
        {
            m_MainVirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = m_DollyPosition;
            m_ZoomedInVirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = m_DollyPosition;
        }
    }
}
