using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitilaizer : MonoBehaviour
{
    public CameraManagement cameraManager;
    private CameraManagement.Cameras CameraEnum;
    public CinemachineVirtualCamera[] cameras;
    public CinemachineVirtualCamera currentCamera;
    // Update is called once per frame
    void Update()
    {
        CameraEnum = cameraManager.currentCamera;
        SwitchToCamera(CameraEnum);
    }

    public void SwitchToCamera(CameraManagement.Cameras chosenCamera)
    {
        switch (chosenCamera)
        {
            case CameraManagement.Cameras.Indoor:
                currentCamera = cameras[0];
                break;
            case CameraManagement.Cameras.Overhead:
                currentCamera = cameras[1];
                break;
            case CameraManagement.Cameras.Window:
                currentCamera = cameras[2];
                break;
        }
        EstablishCamera();
    }

    public void EstablishCamera()
    {
        currentCamera.Priority = 1;

        foreach (var camera in cameras)
        {
            if (camera != currentCamera && camera != null)
            {
                camera.Priority = 0;
            }
        }
    }
}
