using System;
using System.ComponentModel;
using UnityEngine;
using PD3Stars.Models;

namespace PD3Stars.Presenters
{
    public class ColtBulletPresenter : PresenterBaseClass<ColtBullet>
    {
        protected float _damage;
        protected float _speed;

        private Transform _parentTransform;

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        { }

        protected override void FixedUpdate()
        {
            Model.FixedUpdate(Time.fixedDeltaTime);

            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }

        protected override void OnModelChanged(ColtBullet previousModel)
        {
            Model.BulletActivated += OnBulletActivated;
            Model.BulletDeactivated += OnBulletDeactivated;
        }
        private void OnBulletActivated(object sender, EventArgs args)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 0);

            _parentTransform = transform.parent;
            transform.parent = null;

            gameObject.SetActive(true);
        }
        private void OnBulletDeactivated(object sender, EventArgs args)
        {
            gameObject.SetActive(false);

            transform.parent = _parentTransform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Brawler"))
            {
                other.GetComponent<BrawlerPresenter>().Model.Health -= _damage;
                OnBulletDeactivated(this, EventArgs.Empty);
            }
            else
            {
                Model.Deactivate();
            }
        }

        private void Awake()
        {
            _speed = 12;
            _damage = 100;

            _parentTransform = transform.parent;
        }
    }
}