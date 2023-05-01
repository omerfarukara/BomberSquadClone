using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Controllers.Enemy;
using GameFolders.Scripts.Interfaces;
using UnityEngine;

namespace GameFolders.Scripts
{
    public class Missile : Ammo
    {
        private Action<Missile> onComplete;

        private ParticleSystem bombParticle;

        private void Awake()
        {
            bombParticle = GetComponentInChildren<ParticleSystem>();
        }

        internal override void OnInitiate()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            bombParticle.transform.parent = null;
            bombParticle.Play();
            onComplete?.Invoke(this);

            if (other.TryGetComponent(out EnemyController enemy))
            {
                enemy.TakeDamage(damage);
                //Process
            }
        }

        internal override void OnAttack(Action<Ammo> callback)
        {
            //Process
            transform.parent = null;

            #region Bomb Particle Reset Before Attack

            bombParticle.transform.parent = transform;
            bombParticle.transform.localPosition = Vector3.zero;
            bombParticle.transform.localRotation = Quaternion.identity;

            #endregion
            
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ |
                                                    RigidbodyConstraints.FreezeRotationX;
            GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(0).localScale = Vector3.one;
            onComplete = callback;
        }
        
    }
}
