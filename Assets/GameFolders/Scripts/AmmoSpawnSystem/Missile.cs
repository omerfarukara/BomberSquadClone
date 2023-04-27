using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Interfaces;
using UnityEngine;

namespace GameFolders.Scripts
{
    public class Missile : Ammo
    {
        [SerializeField] private float damage;
        private Action<Missile> onComplete;
        
        internal override void OnInitiate()
        {
            transform.parent = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyController enemy))
            {
                enemy.TakeDamage(damage);
                //Process
                onComplete?.Invoke(this);
            }
        }

        internal override void OnAttack(Action<Ammo> callback)
        {
            //Process
            transform.localScale = Vector3.one;
            onComplete = callback;
        }
        
    }
}
