using System;

namespace PD3Stars.UI 
{
    public interface IHealthBar 
    {
        public event EventHandler HealthChanged;
        public float HealthProgress { get; }    // range [0,1]
    }
}