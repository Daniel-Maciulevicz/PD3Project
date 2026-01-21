using PD3Stars.Models;
using PD3Stars.Presenters;
using System;
using UnityEngine.InputSystem;

namespace PD3Stars.Strategies
{
    public class UserAttackStrategy : AttackStrategyBaseClass
    {
        public override event EventHandler AttackStarted;

        public void OnAttackInput(InputAction.CallbackContext context)
        {
            if (context.started)
                AttackStarted.Invoke(this, EventArgs.Empty);
        }

        public UserAttackStrategy(Brawler context, BrawlerPresenter presenterContext) : base(context, presenterContext)
        {
            EnableInputs();
        }
        private void EnableInputs()
        {
            InputActionMap map = InputHandler.Input.actions.FindActionMap("Player");

            map.FindAction("Attack").started += OnAttackInput;
            map.FindAction("Attack").performed += OnAttackInput;
            map.FindAction("Attack").canceled += OnAttackInput;
        }
        ~UserAttackStrategy()
        {
            DisableInputs();
        }
        private void DisableInputs()
        {
            InputActionMap map = InputHandler.Input.actions.FindActionMap("Player");

            map.FindAction("Attack").started -= OnAttackInput;
            map.FindAction("Attack").performed -= OnAttackInput;
            map.FindAction("Attack").canceled -= OnAttackInput;
        }
    }
}