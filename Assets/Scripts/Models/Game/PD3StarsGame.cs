using PD3Stars.Presenters;
using System;
using System.Collections.Generic;

namespace PD3Stars.Models
{
    public class PD3StarsGame : UnityModelBaseClass
    {
        private List<Brawler> _brawlers = new List<Brawler>();

        public event EventHandler<BrawlerSpawnedEventArgs> BrawlerSpawned;

        public void AddColt()
        {
            Colt colt = new Colt();
            _brawlers.Add(colt);

            TrySetPlayer(colt);

            OnBrawlerSpawned(new BrawlerSpawnedEventArgs(colt));
        }

        private void TrySetPlayer(Brawler brawler)
        {
            if (_brawlers.Count == 1)
                Singleton<HUD>.Instance.Brawler = brawler;
        }

        private void OnBrawlerSpawned(BrawlerSpawnedEventArgs args)
        {
            BrawlerSpawned.Invoke(this, args);
        }
    }

    public class BrawlerSpawnedEventArgs : EventArgs
    {
        public Brawler Brawler { get; private set; }

        public BrawlerSpawnedEventArgs(Brawler brawler)
        {
            Brawler = brawler;
        }
    }
}