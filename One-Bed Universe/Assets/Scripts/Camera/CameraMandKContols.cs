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

        //TO DO: Put this is a scriptable object so it can be saved in runtime;
        public KeyCode CameraClockwise = KeyCode.E;
        public KeyCode CameraAntiClockwise = KeyCode.Q;
        public KeyCode CameraZoomIn = KeyCode.W;
        public KeyCode CameraZoomOut = KeyCode.S;

        private void Update()
        {
            if (CameraManager.Instance == null)
            {
                return;
            }

            if (Input.GetKeyDown(CameraClockwise))
            {
                CameraManager.Instance.MoveCamAntiClockwise();
            }

            if (Input.GetKeyDown(CameraAntiClockwise))
            {
                CameraManager.Instance.MoveCamClockwise();
            }

            if (Input.GetKeyDown(CameraZoomIn))
            {
                CameraManager.Instance.UseZoomedInCam();
            }

            if (Input.GetKeyDown(CameraZoomOut))
            {
                CameraManager.Instance.UseMainCam();
            }
        }
    }
}
