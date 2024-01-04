using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[CreateAssetMenu(menuName = "Camera Manager")]
public class CameraManagement : ScriptableObject
{
    [SerializeField]
    public enum Cameras { Indoor, Overhead, Window};
    public Cameras currentCamera;
    public bool changingCameras;

   

}
