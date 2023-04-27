using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [SerializeField] private float bulletCooldown;
    [SerializeField] private MissileSpawner missileSpawner;

    private float attackTimer;
    private PlaneController _planeController;
    private SpriteRenderer _spriteRenderer;
    
    private bool OnAttack { get; set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _planeController = PlaneController.Instance;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag($"Enemy"))
        {
            OnAttack = true;
            _spriteRenderer.color = Color.red;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag($"Enemy"))
        {
            OnAttack = false;
            _spriteRenderer.color = Color.white;
        }
    }

    private void Update()
    {
        AttackProcessWithTimer();

        transform.rotation = Quaternion.Euler(Vector3.right * 90);
        transform.position = new Vector3(_planeController.transform.position.x, transform.position.y,
            _planeController.transform.position.z);
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
    
    private void Attack()
    {
        missileSpawner.ProduceMissile();
    }
}