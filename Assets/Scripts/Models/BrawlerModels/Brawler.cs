using UnityEngine;
using PD3Stars.Models.FSM;

namespace PD3Stars.Models
{
    public abstract class Brawler : UnityModelBaseClass
    {
        protected float _maxHealth = 1000;
        [Tooltip("% / Second")]
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
            }
        }
        private float _health;

        public virtual void HPRegenerate(float deltaTime)
        {
            Health += _regenSpeed * _maxHealth * deltaTime;
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            HPFSM.Update(fixedDeltaTime);
            PAFSM.Update(fixedDeltaTime);
        }

        public abstract void PrimaryAttackRequest();

        public Brawler()
        {
            _health = _maxHealth / 2;
        }
    }
}
