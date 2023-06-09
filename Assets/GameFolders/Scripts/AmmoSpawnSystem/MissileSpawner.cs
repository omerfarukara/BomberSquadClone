using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts;
using GameFolders.Scripts.Controllers.Player;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private Ammo missilePrefab;

    private Queue<Ammo> _missiles = new Queue<Ammo>();

    public void ProduceMissile(int damage)
    {
        if (_missiles.Count == 0)
        {
            Ammo newAmmo = Instantiate(missilePrefab, transform);
            newAmmo.OnInitiate();
            newAmmo.damage = damage;
            _missiles.Enqueue(newAmmo);
        }

        Ammo currentAmmo = _missiles.Dequeue();
        currentAmmo.OnAttack(MissilesReturnToQueue);
        PlaneController.Instance.AmmoCount--;
    }

    private void MissilesReturnToQueue(Ammo ammo)
    {
        Rigidbody ammoRigidbody = ammo.GetComponent<Rigidbody>();

        ammoRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | 
                                    RigidbodyConstraints.FreezeRotationX |
                                    RigidbodyConstraints.FreezePositionY;
        ammoRigidbody.velocity = Vector3.zero;
        ammoRigidbody.useGravity = false;

        ammo.transform.parent = transform;
        ammo.transform.localPosition = Vector3.zero;
        ammo.transform.localRotation =Quaternion.Euler(Vector3.right * 90);

        _missiles.Enqueue(ammo);
        ammo.transform.GetChild(0).localScale = Vector3.zero;
    }
}