using System.ComponentModel;
using UnityEngine;
using PD3Stars.Models;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace PD3Stars.Presenters
{
    public class PD3StarsGamePresenter : PresenterBaseClass<PD3StarsGame>
    {
        [SerializeField]
        private UIDocument _hud;
        [SerializeField]
        private VisualTreeAsset _healthBarUXML;
        [SerializeField]
        private List<Transform> _spawnPoints;
        private int _brawlersSpawned;
        [SerializeField]
        private GameObject _cameraPrefab;

        [Space]
        [Header("Brawler Prefabs")]

        [SerializeField]
        private GameObject _coltPrefab;

        private void Awake()
        {
            Model = new PD3StarsGame();

            Model.AddColt();
        }

        private void OnBrawlerSpawned(object sender, BrawlerSpawnedEventArgs args)
        {
            ColtPresenter presenter = null;

            switch (args.Brawler)
            {
                case Colt colt:

                    GameObject obj = Instantiate(_coltPrefab, _spawnPoints[_brawlersSpawned]);
                    presenter = obj.GetComponent<ColtPresenter>();

                    presenter.Model = colt;

                    break;
            }

            presenter.transform.parent = null;
            presenter.AddHB(_hud, _healthBarUXML);

            if (_brawlersSpawned == 0)
                Instantiate(_cameraPrefab, presenter.transform.position, Quaternion.identity).GetComponent<CameraMovement>()._target = presenter.transform;

            _brawlersSpawned++;
        }

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs args) { }

        protected override void OnModelChanged(PD3StarsGame previousModel)
        {
            base.OnModelChanged(previousModel);
            
            if (previousModel != null)
                previousModel.BrawlerSpawned -= OnBrawlerSpawned;
            Model.BrawlerSpawned += OnBrawlerSpawned;
        }
    }
}
