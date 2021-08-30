using HPP.PlayerControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HPP.CameraControls
{
    /// <summary>
    /// Mouse and keyboard controls for camera
    /// </summary>
    public class CameraMandKContols : MonoBehaviour
    {
        [SerializeField] KeyboardConfig m_KeyboardConfig;
        private void Update()
        {
            if (CameraManager.Instance == null)
            {
                return;
            }

            if (Input.GetKeyDown(m_KeyboardConfig.CameraClockwise))
            {
                CameraManager.Instance.MoveCamAntiClockwise();
            }

            if (Input.GetKeyDown(m_KeyboardConfig.CameraAntiClockwise))
            {
                CameraManager.Instance.MoveCamClockwise();
            }

            if (Input.GetKeyDown(m_KeyboardConfig.CameraZoomIn))
            {
                CameraManager.Instance.UseZoomedInCam();
            }

            if (Input.GetKeyDown(m_KeyboardConfig.CameraZoomOut))
            {
                CameraManager.Instance.UseMainCam();
            }
        }
    }
}
