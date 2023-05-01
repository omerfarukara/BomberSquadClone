using System;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.General.FGEnum;
using UnityEngine;

namespace GameFolders.Scripts.General.Data
{
    [CreateAssetMenu(fileName = "PlaneData", menuName = "Data/Plane Data")]
    public class PlaneData : ScriptableObject
    {
        [SerializeField] internal List<PlaneVariables> planeVariablesList;

        public int GetDamage(DifficultyLevel difficultyLevel)
        {
            return planeVariablesList.FirstOrDefault(x => x.PlaneDifficultyLevel == difficultyLevel).Damage;
        }
    }

    [Serializable]
    public struct PlaneVariables
    {
        [SerializeField] internal int damageUpgradeMoney;
        [SerializeField] internal int healthUpgradeMoney;
        [SerializeField] internal int capacityUpgradeMoney;
        
        
        [SerializeField] private int damage;
        [SerializeField] private int health;
        [SerializeField] private int capacity;

        public int Health
        {
            get => PlayerPrefs.GetInt($"Plane{PlaneDifficultyLevel}Health", health);
            set => PlayerPrefs.SetInt($"Plane{PlaneDifficultyLevel}Health", value);
        }
        public int Capacity
        {
            get => PlayerPrefs.GetInt($"Plane{PlaneDifficultyLevel}Capacity", capacity);
            set => PlayerPrefs.SetInt($"Plane{PlaneDifficultyLevel}Capacity", value);
        }
        public int Damage
        {
            get => PlayerPrefs.GetInt($"Plane{PlaneDifficultyLevel}Damage", damage);
            set => PlayerPrefs.SetInt($"Plane{PlaneDifficultyLevel}Damage", value);
        }

        public DifficultyLevel PlaneDifficultyLevel;
    }
}