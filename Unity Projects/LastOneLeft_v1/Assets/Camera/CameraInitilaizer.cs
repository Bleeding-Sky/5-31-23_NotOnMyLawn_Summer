using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitilaizer : MonoBehaviour
{
    public CameraManagement cameraManager;
    public CameraManagement.Cameras CameraEnum;
    public CinemachineVirtualCamera[] cameras;
    public CinemachineVirtualCamera currentCamera;
    //set this manually in the inspector pls
    [SerializeField] Camera mainCam;

    int indoorCullingMask = 536879095;
    int windowCullingMask = 1610616823;

    void Start()
    {
        //used to identify the culling masks needed for the window/indoor shit
        //Debug.Log("Culling mask = " + GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().cullingMask);
        cameraManager.currentEnum = CameraManagement.Cameras.Indoor;

        //BEGIN ON INDOOR VIEW
        mainCam.cullingMask = indoorCullingMask;

        SwitchToCamera(CameraEnum);
    }
    // Update is called once per frame
    void Update()
    {
        CameraEnum = cameraManager.currentEnum;
        SwitchToCamera(CameraEnum);
    }

    public void SwitchToCamera(CameraManagement.Cameras chosenCamera)
    {
        switch (chosenCamera)
        {
            case CameraManagement.Cameras.Indoor:
                currentCamera = cameras[0];
                cameraManager.currentCamera = currentCamera.gameObject;
                mainCam.cullingMask = indoorCullingMask;
                break;
            case CameraManagement.Cameras.Overhead:
                currentCamera = cameras[1];
                cameraManager.currentCamera = currentCamera.gameObject;
                break;
            case CameraManagement.Cameras.Window:
                currentCamera = cameras[2];
                cameraManager.currentCamera = currentCamera.gameObject;
                mainCam.cullingMask = windowCullingMask;
                break;
            case CameraManagement.Cameras.KitchenWindow:
                currentCamera = cameras[3];
                cameraManager.currentCamera = currentCamera.gameObject;
                mainCam.cullingMask = windowCullingMask;
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
