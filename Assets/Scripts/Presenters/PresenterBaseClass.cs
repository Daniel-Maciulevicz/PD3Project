using PD3Stars.Models;
using System.ComponentModel;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public abstract class PresenterBaseClass<T> : MonoBehaviour where T : UnityModelBaseClass
    {
        public T Model
        {
            get { return _model; }
            set
            {
                T previousModel = null;

                if (_model == value)
                    return;
                if (_model != null)
                {
                    _model.PropertyChanged -= ModelPropertyChanged;
                    previousModel = _model;
                }

                _model = value;
                _model.PropertyChanged += ModelPropertyChanged;

                OnModelChanged(previousModel);
            }
        }
        private T _model;

        protected virtual void Update()
        {
            _model?.Update(Time.deltaTime);
        }
        protected virtual void FixedUpdate()
        {
            _model?.FixedUpdate(Time.fixedDeltaTime);
        }

        protected abstract void ModelPropertyChanged(object sender, PropertyChangedEventArgs args);
        protected virtual void OnModelChanged(T previousModel)
        { }
    }
}
