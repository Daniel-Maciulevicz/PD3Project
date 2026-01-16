using System;
using UnityEngine.VFX;

namespace PD3Stars.Models
{
    public class ColtBullet : UnityModelBaseClass
    {
        public EventHandler BulletDestroyed;

        protected float _destructionTimer = 3;

        public override void FixedUpdate(float fixedDeltaTime)
        {
            _destructionTimer -= fixedDeltaTime;

            if (_destructionTimer <= 0)
                BulletDestroyed.Invoke(this, EventArgs.Empty);
        }
    }

    public class ColtBulletCreatedArgs : EventArgs
    {
        public ColtBullet Bullet { get; private set; }

        public ColtBulletCreatedArgs(ColtBullet bullet)
        {
            Bullet = bullet;
        }
    }
}