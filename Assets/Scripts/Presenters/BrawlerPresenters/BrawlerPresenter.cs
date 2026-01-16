using PD3Stars.Models;
using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PD3Stars.Presenters
{
    public abstract class BrawlerPresenter<T> : PresenterBaseClass<T> where T : Brawler
    {
        [SerializeField]
        private CharacterController _controller;

        protected float _moveSpeed;
        private Vector2 _moveInput;

        protected override void FixedUpdate()
        {
            if (Model.Health <= 0)
                HPReachedZero();

            if (_moveInput != Vector2.zero) Move();

            base.FixedUpdate();
        }

        public virtual void HPReachedZero()
        {
            Destroy(gameObject);
        }

        public void OnAttackInput(InputAction.CallbackContext context)
        {
            Model.PAFSM.CurrentState.PrimaryAttackRequest();
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        private void Move()
        {
            Vector3 moveBy =
                new Vector3(_moveInput.x * _moveSpeed * Time.fixedDeltaTime, 0, _moveInput.y * _moveSpeed * Time.fixedDeltaTime);

            _controller.transform.LookAt(transform.position + moveBy);
            _controller.Move(moveBy);
        }

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        { }

        #region Inputs
        private bool _started;

        private void Start()
        {
            EnableInputs();

            _started = true;
        }
        private void OnEnable()
        {
            if (_started)
                EnableInputs();
        }
        private void EnableInputs()
        {
            InputActionMap map = InputManager.Input.actions.FindActionMap("Player");

            map.FindAction("Move").started += OnMoveInput;
            map.FindAction("Move").performed += OnMoveInput;
            map.FindAction("Move").canceled += OnMoveInput;
            map.FindAction("Attack").started += OnAttackInput;
            map.FindAction("Attack").performed += OnAttackInput;
            map.FindAction("Attack").canceled += OnAttackInput;
        }
        private void OnDisable()
        {
            DisableInputs();
        }
        private void DisableInputs()
        {
            if (InputManager.Input == null)
                return;

            InputActionMap map = InputManager.Input.actions.FindActionMap("Player");

            map.FindAction("Move").started -= OnMoveInput;
            map.FindAction("Move").performed -= OnMoveInput;
            map.FindAction("Move").canceled -= OnMoveInput;
            map.FindAction("Attack").started -= OnAttackInput;
            map.FindAction("Attack").performed -= OnAttackInput;
            map.FindAction("Attack").canceled -= OnAttackInput;
        }
        #endregion Inputs
    }
}