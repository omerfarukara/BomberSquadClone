using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaneEventData", menuName = "Data/Plane Event Data")]
public class PlaneEventData : ScriptableObject
{
    public Action TakeOff { get; set; }
}
