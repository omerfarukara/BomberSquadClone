using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.Components.Money;
using GameFolders.Scripts.General.Data;
using GameFolders.Scripts.General.FGEnum;
using GameFolders.Scripts.Interfaces;
using UnityEngine;

namespace GameFolders.Scripts.Controllers.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform enemies;
        [SerializeField] private DifficultyLevel difficultyLevel;
        [SerializeField] private float bulletCooldown;

        private EnemyData _enemyData;
        private MoneyCreator _moneyCreator;
        private BulletSpawner _bulletSpawner;
        private Animator _animator;

        private bool OnAttack { get; set; }

        private float _healt;
        public float Health
        {
            get => _healt;
            set
            {
                _healt = value;
                if (value <= 0)
                {
                    _moneyCreator.MoneyCreate();
                    StartCoroutine(Dead());
                }
            }
        }
    
        public int Damage { get; set; }


        private float attackTimer;

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
                _animator.SetBool("Fire", true);
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
                _animator.SetBool("Fire", false);
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
            _bulletSpawner.ProduceBullets(Damage);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }

        private void OnValidate()
        {
            SetDataVariables();
        }

        private void SetDataVariables()
        {
            for (int i = 0; i < enemies.childCount; i++)
            {
                enemies.GetChild(i).gameObject.SetActive(enemies.GetChild(i).GetComponent<Components.Enemy.Enemy>().difficultyLevel == 
                                                         difficultyLevel);
            }

            foreach (var data in enemies.GetComponents<Components.Enemy.Enemy>())
            {
                if (data.difficultyLevel == difficultyLevel)
                {
                    print(data + "True");
                    data.gameObject.SetActive(true);
                }
                print(data + "False");
                data.gameObject.SetActive(false);
            }
            
            _enemyData = Resources.Load("EnemyData") as EnemyData;
            if (_enemyData != null)
            {
                EnemyVariables enemyVariables = _enemyData.enemyVariablesList.FirstOrDefault(e => e.EnemyDifficultyLevel == difficultyLevel);
                Health = enemyVariables.Health;
                Damage = _enemyData.GetDamage(difficultyLevel);
            }
        }
    }
}