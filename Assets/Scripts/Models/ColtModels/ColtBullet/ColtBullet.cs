using System;

namespace PD3Stars.Models
{
    public class ColtBullet : UnityModelBaseClass, IPoolItem
    {
        public EventHandler BulletActivated;
        public EventHandler BulletDeactivated;

        protected float _destructionTimer = 3;

        public void Activate()
        {
            BulletActivated.Invoke(this, EventArgs.Empty);
        }
        public void Reset()
        {
            _destructionTimer = 3;
        }
        public void Deactivate()
        {
            BulletDeactivated.Invoke(this, EventArgs.Empty);
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            _destructionTimer -= fixedDeltaTime;

            if (_destructionTimer <= 0)
                Deactivate();
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