using System;
using System.Collections;
using UnityEngine;

namespace GameFolders.Scripts
{
    public abstract class Ammo : MonoBehaviour
    {
        public float damage;

        internal abstract void OnInitiate();
        internal abstract void OnAttack(Action<Ammo> callback);
    }
}
