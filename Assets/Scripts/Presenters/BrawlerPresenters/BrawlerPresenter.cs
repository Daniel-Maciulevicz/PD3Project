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
        private GameObject _healthBarPrefab;

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        { }

        [SerializeField]
        protected CharacterController _controller;

        public MovementStrategyBaseClass MovementStrategy { get; set; }
        public AttackStrategyBaseClass AttackStrategy { get; set; }

        private Vector3 _spawnPos;

        protected float _moveSpeed = 5;

        private HealthBarCanvasPresenter _hbPresenter;
        private HUDElementPresenter _hudPresenter;

        protected override void FixedUpdate()
        {
            MovementStrategy.FixedUpdate(Time.fixedDeltaTime);
            if (Model.CanMove && MovementStrategy.MoveInput != Vector2.zero) 
                Move(_controller, MovementStrategy.MoveInput);
            AttackStrategy.FixedUpdate(Time.fixedDeltaTime);

            base.FixedUpdate();
        }
        protected virtual void LateUpdate()
        {
            _hbPresenter?.UpdatePosition();
        }

        private void OnHealthChanged(object sender, EventArgs args)
        {
            if (Model.Health <= 0)
                Model.DeathRequest();
        }
        private void OnRespawned(object sender, EventArgs args)
        {
            transform.position = _spawnPos;
        }

        public void AddHB()
        {
            GameObject hb = Instantiate(_healthBarPrefab, transform.Find("HealthBar Anchor"));
            _hbPresenter = hb.GetComponent<HealthBarCanvasPresenter>();
            _hbPresenter.Model = Model;
            hb.GetComponent<Canvas>().worldCamera = Camera.main;
            hb.GetComponent<RectTransform>().localPosition = Vector2.zero;
        }
        public void Move(CharacterController controller, Vector2 moveInput)
        {
            Vector3 moveBy =
                new Vector3(moveInput.x * _moveSpeed * Time.fixedDeltaTime, 0, moveInput.y * _moveSpeed * Time.fixedDeltaTime);

            controller.transform.LookAt(controller.transform.position + moveBy);
            controller.Move(moveBy);
        }
        private void RequestAttack(object sender, EventArgs args)
        {
            Model.PAFSM.CurrentState.PrimaryAttackRequest();
        }

        protected virtual void Start()
        {
            AttackStrategy.AttackStarted += RequestAttack;

            Model.HealthChanged += OnHealthChanged;
            Model.Respawned += OnRespawned;

            _spawnPos = transform.position;

            _hudPresenter = new HUDElementPresenter(Model);
        }
    }
}