using PD3Stars.Models.FSM;
using PD3Stars.UI;
using System;

namespace PD3Stars.Models
{
    public abstract class Brawler : UnityModelBaseClass, IHealthBar
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

                OnPropertyChanged();
                HealthChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private float _health;
        public float HealthProgress
        {
            get 
            { 
                return Health / _maxHealth; 
            }
        }

        public event EventHandler HealthChanged;
        public event EventHandler Respawned;

        public bool CanMove { get; set; }

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