using PD3Stars.Models;
using PD3Stars.Presenters;
using System.ComponentModel;
using UnityEngine;

namespace PD3Stars.Strategies
{
    public abstract class BrawlerStrategyBaseClass : IBrawlerStrategy
    {
        public virtual Brawler Context
        {
            get { return _context; }
            set
            {
                if (_context == value) return;
                if (_context != null)
                    _context.PropertyChanged -= ContextPropertyChanged;

                _context = value;
                _context.PropertyChanged += ContextPropertyChanged;
            }
        }
        private Brawler _context;

        public virtual BrawlerPresenter PresenterContext
        {
            get { return _presenterContext; }
            set
            {
                if (_presenterContext == value) return;
                _presenterContext = value;
            }
        }
        private BrawlerPresenter _presenterContext;

        public virtual void FixedUpdate(float fixedDeltaTime) { }
        public virtual void Update(float deltaTime) { }

        protected virtual void ContextPropertyChanged(object sender, PropertyChangedEventArgs args) { }

        public BrawlerStrategyBaseClass(Brawler context, BrawlerPresenter presenterContext)
        {
            Context = context;
            PresenterContext = presenterContext;
        }
    }
}