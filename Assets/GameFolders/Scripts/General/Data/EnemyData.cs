using System;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.General.FGEnum;
using UnityEngine;

namespace GameFolders.Scripts.General.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] internal List<EnemyVariables> enemyVariablesList;

        public int GetDamage(DifficultyLevel difficultyLevel)
        {
            return enemyVariablesList.FirstOrDefault(x => x.EnemyDifficultyLevel == difficultyLevel).Damage;
        }
    }

    [Serializable]
    public struct EnemyVariables
    {
        public int Health;
        public int Damage;
        public DifficultyLevel EnemyDifficultyLevel;
    }
}