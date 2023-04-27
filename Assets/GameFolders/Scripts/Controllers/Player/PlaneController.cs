using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using GameFolders.Scripts.Interfaces;
using UnityEngine;

public class PlaneController : MonoSingleton<PlaneController>,IDamageable
{
    private float _healt;
    public float Health
    {
        get => _healt;
        set
        {
            _healt = value;
            if (value <= 0)
            {
                //Dead
            }
        }
    }

    private void Awake()
    {
        Singleton();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
