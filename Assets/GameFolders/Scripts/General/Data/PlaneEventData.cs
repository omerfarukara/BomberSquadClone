using System;
using UnityEngine;

namespace GameFolders.Scripts.General.Data
{
    [CreateAssetMenu(fileName = "PlaneEventData", menuName = "Data/Plane Event Data")]
    public class PlaneEventData : ScriptableObject
    {
        public Action Landing { get; set; }
        public Action TakeOff { get; set; }
        public Action ReturnToBase { get; set; }
        
    }
}
