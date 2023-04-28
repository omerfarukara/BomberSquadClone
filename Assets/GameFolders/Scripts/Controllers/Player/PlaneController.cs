using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFolders.Scripts.General;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.Managers;
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

    private EventData _eventData;
    
    private void Awake()
    {
        Singleton();
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void OnEnable()
    {
        _eventData.CollectMoney += CollectMoney;
    }

    private void OnDisable()
    {
        _eventData.CollectMoney -= CollectMoney;
    }
    
    private void CollectMoney(GameObject obj)
    {
        obj.transform.parent = transform;
        obj.transform.DOLocalJump(Vector3.zero, 5f,2,0.5f).OnComplete(() =>
        {
            GameManager.Instance.Money += 1;
            Destroy(obj);
        }); // Parayı gönderke
    }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
