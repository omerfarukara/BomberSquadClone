using System;
using GameFolders.Scripts.General.FGEnum;
using UnityEngine;

namespace GameFolders.Scripts.General
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Data/Event Data")]
    public class EventData : ScriptableObject
    {
        public Action OnPlay { get; set; }
        public Action OnFinishLevel { get; set; }
        public Action OnLoseLevel { get; set; }
        public Action PlayCamera { get; set; }
        public Action<PlaneState> ChangePlaneState { get; set; }
        
        public Action<GameObject> CollectMoney { get; set; }
    }
}