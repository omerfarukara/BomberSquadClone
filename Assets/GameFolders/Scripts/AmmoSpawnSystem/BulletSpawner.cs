using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Ammo bulletPrefab;

    [ShowInInspector]
    private Queue<Ammo> _bullets = new Queue<Ammo>();

    public void ProduceBullets(int damage)
    {
        if (_bullets.Count == 0)
        {
            Ammo newAmmo = Instantiate(bulletPrefab,transform);
            newAmmo.OnInitiate();
            _bullets.Enqueue(newAmmo);
        }
        
        Ammo currentAmmo = _bullets.Dequeue();
        currentAmmo.damage = damage;
        currentAmmo.OnAttack(BulletsReturnToQueue);
    }
    
    private void BulletsReturnToQueue(Ammo ammo)
    {
        ammo.transform.parent = transform;
        ammo.transform.localPosition = Vector3.zero;

        _bullets.Enqueue(ammo);
        ammo.transform.localScale = Vector3.zero;
    }
}
