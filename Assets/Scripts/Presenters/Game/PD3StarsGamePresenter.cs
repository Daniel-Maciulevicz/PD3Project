using System.ComponentModel;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public class PD3StarsGamePresenter : PresenterBaseClass<PD3StarsGame>
    {
        [SerializeField]
        private GameObject _brawler;
        [SerializeField]
        private GameObject _camera;
        [SerializeField]
        private Transform _spawnPoint;

        private void Awake()
        {
            Model = new PD3StarsGame();

            Model.SpawnPlayer(_brawler, _camera, _spawnPoint);
        }

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        { }
    }
}
