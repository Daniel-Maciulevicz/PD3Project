using PD3Stars.Models;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public class ColtPresenter : BrawlerPresenter<Colt>
    {
        [SerializeField]
        private GameObject _coltBullet;

        private void CreateBullet(object sender, ColtBulletCreatedArgs args)
        {
            GameObject bullet = Instantiate(_coltBullet, transform.position, transform.rotation);
            bullet.GetComponent<ColtBulletPresenter>().Model = args.Bullet;
        }

        protected override void OnModelChanged(Colt previousModel)
        {
            Model.ColtBulletCreated += CreateBullet;
        }
    }
}