using PD3Stars.Models.FSM;
using PD3Stars.UI;
using System;

namespace PD3Stars.Models
{
    public abstract class Brawler : UnityModelBaseClass, IHealthBar, IHUDElement
    {
        public int ID { get; set; }

        protected float _maxHealth = 1000;
        protected float _regenSpeed = 0.13f;

        public BrawlerHPFSM HPFSM { get; set; }
        public BrawlerPAFSM PAFSM { get; set; }

        public float Health
        {
            get { return _health; }
            set
            {
                if (_health == value) return;

                if (value < _health)
                {
                    HPFSM.CurrentState = HPFSM.CooldownState;
                    PAFSM.CurrentState = PAFSM.CooldownState;
                }

                if (value > _maxHealth)
                    _health = _maxHealth;
                else if (value < 0)
                    _health = 0;
                else
                    _health = value;

                HealthProgress = Health / _maxHealth;

                OnPropertyChanged();
                HealthChanged.Invoke(this, EventArgs.Empty);
            }
        }
        private float _health;
        public float HealthProgress 
        { 
            get { return _healthProgress; }
            set
            {
                if (_healthProgress == value) return;

                if (value > 1)
                    _healthProgress = 1;
                else if (value < 0)
                    _healthProgress = 0;
                else
                    _healthProgress = value;

                HealthProgressChanged?.Invoke(this, new HUDValueChangedArgs(_healthProgress));
            }
        }
        private float _healthProgress;
        public float ReloadProgress 
        { 
            get { return _reloadProgress; }
            set
            {
                if (_reloadProgress == value) return;

                if (value > 1)
                    _reloadProgress = 1;
                else if (value < 0)
                    _reloadProgress = 0;
                else
                    _reloadProgress = value;

                ReloadProgressChanged?.Invoke(this, new HUDValueChangedArgs(_reloadProgress));
            }
        }
        private float _reloadProgress;

        public event EventHandler Respawned;

        public event EventHandler HealthChanged;
        public event EventHandler<HUDValueChangedArgs> HealthProgressChanged;
        public event EventHandler<HUDValueChangedArgs> ReloadProgressChanged;

        public bool CanMove { get; set; }
        public int HUDNumber { get; set; }

        public virtual void HPRegenerate(float deltaTime)
        {
            Health += _regenSpeed * _maxHealth * deltaTime;
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            HPFSM.Update(fixedDeltaTime);
            PAFSM.Update(fixedDeltaTime);
        }

        public virtual void DeathRequest()
        {
            HPFSM.CurrentState = HPFSM.DeadState;
            PAFSM.CurrentState = PAFSM.DeadState;
        }
        public virtual void RespawnRequest()
        {
            Health = _maxHealth;

            HPFSM.CurrentState = HPFSM.CooldownState;
            PAFSM.CurrentState = PAFSM.CooldownState;

            Respawned.Invoke(this, EventArgs.Empty);
        }
        public abstract void PrimaryAttackRequest();

        public Brawler()
        {
            _health = _maxHealth;

            CanMove = true;
        }
    }
}