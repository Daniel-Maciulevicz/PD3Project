using PD3Stars.Models;
using PD3Stars.UI;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace PD3Stars.Presenters
{
    public abstract class BrawlerPresenter<T> : PresenterBaseClass<T> where T : Brawler
    {
        [SerializeField]
        private CharacterController _controller;

        private Vector2 _moveInput;

        private UIDocument _hudDocument;
        private HealthBarPresenter _hbPresenter;

        protected override void FixedUpdate()
        {
            if (Model.Health <= 0)
                HPReachedZero();

            if (_moveInput != Vector2.zero) Model.Move(_controller, _moveInput);

            base.FixedUpdate();
        }
        protected virtual void LateUpdate()
        {
            _hbPresenter?.UpdatePosition();
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

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        { }

        public void AddHB(UIDocument hudDocument, VisualTreeAsset hbUXML)
        {
            _hudDocument = hudDocument;
            VisualElement cloneRoot = hbUXML.CloneTree();

            Transform hbTransform = transform.Find("HealthBar Anchor");
            if (hbTransform != null)
                _hbPresenter = new HealthBarPresenter(Model, hbTransform, cloneRoot, _hudDocument);
        }

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