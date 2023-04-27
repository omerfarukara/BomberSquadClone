using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private Ammo missilePrefab;

    private Queue<Ammo> _missiles = new Queue<Ammo>();

    public void ProduceMissile()
    {
        if (_missiles.Count == 0)
        {
            Ammo newAmmo = Instantiate(missilePrefab,transform);
            newAmmo.OnInitiate();
            _missiles.Enqueue(newAmmo);
        }
        
        Ammo currentAmmo = _missiles.Dequeue();
        currentAmmo.OnAttack(MissilesReturnToQueue);
    }
    
    private void MissilesReturnToQueue(Ammo ammo)
    {
        ammo.transform.parent = transform;
        ammo.transform.localPosition = Vector3.zero;
        _missiles.Enqueue(ammo);
        ammo.transform.localScale = Vector3.zero;
    }

}
