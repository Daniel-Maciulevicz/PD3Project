using System;
using System.ComponentModel;
using UnityEngine;
using PD3Stars.Models;

namespace PD3Stars.Presenters
{
    public class ColtBulletPresenter : PresenterBaseClass<ColtBullet>
    {
        protected float _speed = 12;

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        { }

        protected override void FixedUpdate()
        {
            Model.FixedUpdate(Time.fixedDeltaTime);

            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }

        protected override void OnModelChanged(ColtBullet previousModel)
        {
            Model.BulletDestroyed += OnBulletDestroyed;
        }

        private void OnBulletDestroyed(object sender, EventArgs args)
        {
            Destroy(gameObject);
        }
    }
}