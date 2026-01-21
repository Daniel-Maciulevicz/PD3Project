using System.ComponentModel;
using UnityEngine;
using PD3Stars.Models;
using UnityEngine.UIElements;
using System.Collections.Generic;
using PD3Stars.Strategies;

namespace PD3Stars.Presenters
{
    public class PD3StarsGamePresenter : PresenterBaseClass<PD3StarsGame>
    {
        [SerializeField]
        private UIDocument _hud;
        [SerializeField]
        private VisualTreeAsset _healthBarUXML;
        [SerializeField]
        private GameObject _cameraPrefab;

        [Space]
        [Header("Spawning")]

        [SerializeField]
        private List<Transform> _spawnPoints;
        [SerializeField]
        private bool _randomizeSpawns;
        private int _brawlersSpawned;
        [SerializeField]
        private string _spawnPlayer;
        [SerializeField]
        private List<string> _spawnBots;

        [Space]
        [Header("Brawler Prefabs")]

        [SerializeField]
        private GameObject _coltPrefab;
        [SerializeField]
        private GameObject _elPrimoPrefab;

        private void Awake()
        {
            if (_spawnPlayer == null)
                Destroy(gameObject);

            Model = Singleton<PD3StarsGame>.Instance;

            if (_randomizeSpawns)
            {
                for (int v1 = _spawnPoints.Count - 1; v1 >= 0; v1--)
                {
                    int v2 = Random.Range(0, _spawnPoints.Count - 1);
                    Transform point = _spawnPoints[v2];
                    _spawnPoints[v2] = _spawnPoints[v1];
                    _spawnPoints[v1] = point;
                }
            }

            SpawnBrawler(_spawnPlayer);
            foreach (string name in _spawnBots)
            {
                SpawnBrawler(name);
            }
        }

        private void SpawnBrawler(string name)
        {
            switch (name)
            {
                case "Colt":
                    Model.Add(new Colt());
                    break;
                case "ElPrimo":
                    Model.Add(new ElPrimo());
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }

        private void OnBrawlerSpawned(object sender, BrawlerSpawnedEventArgs args)
        {
            if (_brawlersSpawned >= _spawnPoints.Count)
                return;

            GameObject brawlerObj = null;
            BrawlerPresenter presenter = null;
            Brawler brawler = null;

            switch (args.Brawler)
            {
                case Colt colt:

                    brawlerObj = Instantiate(_coltPrefab, _spawnPoints[_brawlersSpawned]);
                    presenter = brawlerObj.GetComponent<ColtPresenter>();
                    brawler = colt;

                    break;

                case ElPrimo elPrimo:

                    brawlerObj = Instantiate(_elPrimoPrefab, _spawnPoints[_brawlersSpawned]);
                    presenter = brawlerObj.GetComponent<ElPrimoPresenter>();
                    brawler = elPrimo;

                    break;
            }

            presenter.Model = brawler;
            presenter.transform.parent = null;
            presenter.AddHB(_hud, _healthBarUXML);

            if (_brawlersSpawned == 0)
            {
                presenter.MovementStrategy = new UserMovementStrategy(brawler, presenter);
                presenter.AttackStrategy = new UserAttackStrategy(brawler, presenter);

                Instantiate(_cameraPrefab, brawlerObj.transform.position, Quaternion.identity).GetComponent<CameraMovement>()._target = brawlerObj.transform;
            }
            else
            {
                presenter.MovementStrategy = new CirclesMovementStrategy(brawler, presenter);
                presenter.AttackStrategy = new ASAPAttackStrategy(brawler, presenter);
            }

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
