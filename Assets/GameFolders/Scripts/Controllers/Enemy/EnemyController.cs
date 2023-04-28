using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Components.Money;
using GameFolders.Scripts.Interfaces;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private float healt;
    [SerializeField] private float bulletCooldown;

    private MoneyCreator _moneyCreator;
    private BulletSpawner _bulletSpawner;
    private Animator _animator;

    private float attackTimer;
    private bool OnAttack { get; set; }
    public float Health
    {
        get => healt;
        set
        {
            healt = value;
            if (value <= 0)
            {
                _moneyCreator.MoneyCreate();
                StartCoroutine(Dead());
            }
        }
    }

    private void Awake()
    {
        _moneyCreator = GetComponentInChildren<MoneyCreator>();
        _bulletSpawner = GetComponent<BulletSpawner>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Sight"))
        {
            OnAttack = true;
            _animator.SetBool("Fire",true);
        }
    }

    private void Update()
    {
        AttackProcessWithTimer();
    }

    private void AttackProcessWithTimer()
    {
        if (OnAttack)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = bulletCooldown;
            } 
        }
        else
        {
            attackTimer = bulletCooldown;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag($"Sight"))
        {
            OnAttack = false;
            _animator.SetBool("Fire",false);
        }
    }
    
    private IEnumerator Dead()
    {
        _animator.SetTrigger("Dead");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    
    private void Attack()
    {
        _bulletSpawner.ProduceBullets();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}