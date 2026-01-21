using System;
using System.Collections.Generic;

namespace PD3Stars.Models
{
    public class PD3StarsGame : UnityModelBaseClass
    {
        private List<Brawler> _brawlers = new List<Brawler>();

        public event EventHandler<BrawlerSpawnedEventArgs> BrawlerSpawned;

        public void Add(Brawler brawler)
        {
            _brawlers.Add(brawler);

            OnBrawlerSpawned(new BrawlerSpawnedEventArgs(brawler));
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