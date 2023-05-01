using System;
using DG.Tweening;
using GameFolders.Scripts.Controllers.Player;
using UnityEngine;

namespace GameFolders.Scripts.AmmoSpawnSystem
{
    public class Bullet : Ammo
    {
        [SerializeField] private float time;
        private Action<Bullet> onComplete;

        private PlaneController _plane;
        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlaneController plane))
            {
                IsActive = false;
                plane.TakeDamage(damage);
                onComplete?.Invoke(this);
            }
        }

        private void Update()
        {
            if (!IsActive) return;
            transform.LookAt(_plane.transform.position);
        }


        internal override void OnInitiate()
        {
            _plane = PlaneController.Instance;
        }

        internal override void OnAttack(Action<Ammo> callback)
        {
            //Process
            transform.localScale = Vector3.one;

            transform.parent =_plane.transform; // Mermiyi göndermeden önce parent'a alıyoruz ki hareket halindeki uçağı takip edebilsin
            transform.DOLocalJump(Vector3.zero, 3f,2,time); // Mermiyi gönderke
            IsActive = true;
            onComplete = callback;
        }
    }
}