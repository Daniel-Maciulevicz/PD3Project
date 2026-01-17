using PD3Stars.Models.FSM;
using PD3Stars.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace PD3Stars.Models
{
    public abstract class Brawler : UnityModelBaseClass, IHealthBar
    {
        protected float _maxHealth = 1000;
        [Tooltip("Percent / Second")]
        protected float _regenSpeed = 0.13f;

        protected float _moveSpeed = 5;

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

        public void Move(CharacterController controller, Vector2 moveInput)
        {
            Vector3 moveBy =
                new Vector3(moveInput.x * _moveSpeed * Time.fixedDeltaTime, 0, moveInput.y * _moveSpeed * Time.fixedDeltaTime);

            controller.transform.LookAt(controller.transform.position + moveBy);
            controller.Move(moveBy);
        }

        public Brawler()
        {
            _health = _maxHealth;
        }
    }
}
