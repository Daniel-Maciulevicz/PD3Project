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

        private void Awake()
        {
            Model = new Colt();
            Model.ColtBulletCreated += CreateBullet;

            _moveSpeed = 5;
        }
    }
}