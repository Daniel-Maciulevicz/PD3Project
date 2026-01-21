using PD3Stars.Models;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public class ColtPresenter : BrawlerPresenter
    {
        [SerializeField]
        private GameObject _coltBullet;

        private void CreateBullet(object sender, ColtBulletCreatedArgs args)
        {
            GameObject bullet = Instantiate(_coltBullet, transform);
            bullet.GetComponent<ColtBulletPresenter>().Model = args.Bullet;

            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), _controller);
        }

        protected override void OnModelChanged(Brawler previousModel)
        {
            (Model as Colt).BulletPool.ColtBulletCreated += CreateBullet;
            (Model as Colt).BulletPool.GeneratePool(20);
        }
    }
}