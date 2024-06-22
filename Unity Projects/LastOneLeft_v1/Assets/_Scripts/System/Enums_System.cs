using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//region enum for config
public enum DmgRegionEnum { Crit, Armored, Weak };

public enum ViewEnum { Overhead, Window, Indoor };

public enum FodderStatus { 
        Idle, 
        Stumbling, 
        Stunned, 
        FallingForward, FallenFaceDown, PushUpRecover, 
        FallingBackward, FallenFaceUp, SitUpRecover, 
        Enraging, Enraged, 
        LegsBreaking, Crawling};

public enum FodderLimb { Head, LArm, RArm, Body, Legs };

public enum LimbCondition { Intact, Broken };

public class Enums_System : MonoBehaviour
{
    
}
