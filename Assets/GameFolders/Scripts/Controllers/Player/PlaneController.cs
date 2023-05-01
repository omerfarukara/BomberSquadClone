using System;
using System.Linq;
using DG.Tweening;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Data;
using GameFolders.Scripts.General.FGEnum;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.Managers;
using UnityEngine;

namespace GameFolders.Scripts.Controllers.Player
{
    public class PlaneController : MonoSingleton<PlaneController>,IDamageable
    {
        #region Fields And Properties

        [SerializeField] private Transform planes;
        [SerializeField] private MissileSpawner missileSpawner;
        [SerializeField] internal DifficultyLevel difficultyLevel;
        [SerializeField] private Transform airport;
        
        private PlaneData _planeData;
        private PlaneEventData _planeEventData;
        
        internal PlaneVariables planeVariables;
        
        private float _healt;
        public float Health
        {
            get => _healt;
            set
            {
                _healt = value;
                if (value < 30)
                {
                    _planeEventData.ReturnToBase?.Invoke();
                }
                if (value <= 0)
                {
                    //Dead
                }
            }
        }
        
        private int _ammoCount;
        public int AmmoCount
        {
            get => _ammoCount;
            set
            {
                _ammoCount = value;
                if (AmmoCount <= 0)
                {
                    //Process
                    _planeEventData.ReturnToBase?.Invoke();
                }
            }
        }
    
        public int Damage { get; set; }

        private bool returnToBase;

        #endregion
       
        private EventData _eventData;
    
        private void Awake()
        {
            Singleton();
            _planeEventData = Resources.Load("Plane/PlaneEventData") as PlaneEventData;
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.CollectMoney += CollectMoney;
            _planeEventData.ReturnToBase += ReturnToBase;
            _planeEventData.Landing += Landing;
        }

        private void Landing()
        {
            Health = planeVariables.Health;
        }

        private void Update()
        {
            if (!returnToBase) return;
            
            Vector3 planePos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 airportPos = new Vector3(airport.position.x, 0, airport.position.z);
            float distance = Mathf.Abs(Vector3.Distance(planePos, airportPos));
            
            if (distance < 10)
            {
                returnToBase = false;
                _planeEventData.Landing?.Invoke();
            }
        }

        private void OnDisable()
        {
            _eventData.CollectMoney -= CollectMoney;
            _planeEventData.ReturnToBase -= ReturnToBase;
            _planeEventData.Landing -= Landing;
        }
        
        private void ReturnToBase()
        {
            returnToBase = true;
        }
    
        private void CollectMoney(GameObject obj)
        {
            obj.transform.parent = transform;
            obj.transform.DOLocalJump(Vector3.zero, 5f,2,0.5f).OnComplete(() =>
            {
                GameManager.Instance.Money += 1;
                Destroy(obj);
            });
        }

        public void Attack()
        {
            missileSpawner.ProduceMissile(Damage);
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
            for (int i = 0; i < planes.childCount; i++)
            {
                planes.GetChild(i).gameObject.SetActive(planes.GetChild(i).GetComponent<Components.Player.Plane>().difficultyLevel == 
                                                        difficultyLevel);
            }

            foreach (var data in planes.GetComponents<Components.Player.Plane>())
            {
                if (data.difficultyLevel == difficultyLevel)
                {
                    print(data + "True");
                    data.gameObject.SetActive(true);
                }
                print(data + "False");
                data.gameObject.SetActive(false);
            }
            
            _planeData = Resources.Load("PlaneData") as PlaneData;
            if (_planeData != null)
            {
                planeVariables = _planeData.planeVariablesList.FirstOrDefault(e => e.PlaneDifficultyLevel == difficultyLevel);
                Health = planeVariables.Health;
                AmmoCount = planeVariables.Capacity;
                Damage = _planeData.GetDamage(difficultyLevel);
            }
        }
    }
}
