using PD3Stars.Models;
using PD3Stars.Presenters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PD3Stars.Strategies
{
    public class UserMovementStrategy : MovementStrategyBaseClass
    {
        public void OnMoveInput(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public UserMovementStrategy(Brawler context, BrawlerPresenter presenterContext) : base(context, presenterContext)
        {
            EnableInputs();
        }
        private void EnableInputs()
        {
            InputActionMap map = InputHandler.Input.actions.FindActionMap("Player");

            map.FindAction("Move").started += OnMoveInput;
            map.FindAction("Move").performed += OnMoveInput;
            map.FindAction("Move").canceled += OnMoveInput;
        }
        ~UserMovementStrategy()
        {
            DisableInputs();
        }
        private void DisableInputs()
        {
            InputActionMap map = InputHandler.Input.actions.FindActionMap("Player");

            map.FindAction("Move").started -= OnMoveInput;
            map.FindAction("Move").performed -= OnMoveInput;
            map.FindAction("Move").canceled -= OnMoveInput;
        }
    }
}