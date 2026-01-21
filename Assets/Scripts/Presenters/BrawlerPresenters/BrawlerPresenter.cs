using PD3Stars.Models;
using PD3Stars.Strategies;
using PD3Stars.UI;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace PD3Stars.Presenters
{
    public abstract class BrawlerPresenter : PresenterBaseClass<Brawler>
    {
        [SerializeField]
        private CharacterController _controller;

        public MovementStrategyBaseClass MovementStrategy { get; set; }
        public AttackStrategyBaseClass AttackStrategy { get; set; }

        private UIDocument _hudDocument;
        private HealthBarPresenter _hbPresenter;

        protected float _moveSpeed = 5;

        protected override void FixedUpdate()
        {
            if (Model.Health <= 0)
                HPReachedZero();

            MovementStrategy.FixedUpdate(Time.fixedDeltaTime);
            if (MovementStrategy.MoveInput != Vector2.zero) Move(_controller, MovementStrategy.MoveInput);
            AttackStrategy.FixedUpdate(Time.fixedDeltaTime);

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

        public void RequestAttack(object sender, EventArgs args)
        {
            Model.PAFSM.CurrentState.PrimaryAttackRequest();
        }

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        { }

        public void Move(CharacterController controller, Vector2 moveInput)
        {
            Vector3 moveBy =
                new Vector3(moveInput.x * _moveSpeed * Time.fixedDeltaTime, 0, moveInput.y * _moveSpeed * Time.fixedDeltaTime);

            controller.transform.LookAt(controller.transform.position + moveBy);
            controller.Move(moveBy);
        }

        public void AddHB(UIDocument hudDocument, VisualTreeAsset hbUXML)
        {
            _hudDocument = hudDocument;
            VisualElement cloneRoot = hbUXML.CloneTree();

            Transform hbTransform = transform.Find("HealthBar Anchor");
            if (hbTransform != null)
                _hbPresenter = new HealthBarPresenter(Model, hbTransform, cloneRoot, _hudDocument);
        }

        private void Start()
        {
            AttackStrategy.AttackStarted += RequestAttack;
        }
    }
}