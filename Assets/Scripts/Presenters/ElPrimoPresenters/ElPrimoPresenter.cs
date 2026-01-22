using PD3Animations;
using PD3Stars.Models;
using System;
using UnityEngine;
using UnityEngine.VFX;

namespace PD3Stars.Presenters
{
    public class ElPrimoPresenter : BrawlerPresenter
    {
        [SerializeField]
        private GameObject _elPrimoAttackHitboxPrefab;

        private ElPrimoAttackHitbox _attackHitbox;

        private Animation<float> _dashAnimation;

        private void OnDashStarted(object sender, EventArgs args)
        {
            _attackHitbox.Activate();

            StartCoroutine(_dashAnimation.Start());
        }
        public void OnDashValueChanged(object sender, float deltaDistance) 
        {
            _controller.Move(_controller.transform.forward * deltaDistance);
        }
        private void OnDashFinished(object sender, EventArgs args)
        {
            _attackHitbox.Deactivate();
        }

        protected override void Start()
        {
            base.Start();

            _attackHitbox = Instantiate(_elPrimoAttackHitboxPrefab, transform).GetComponent<ElPrimoAttackHitbox>();
            Physics.IgnoreCollision(_attackHitbox.GetComponent<Collider>(), _controller);
            _attackHitbox.Deactivate();

            _dashAnimation = new Animation<float>(0, (Model as ElPrimo).DashDistance, (Model as ElPrimo).DashTime, Mathf.Lerp, EaseStyle.QuartEaseOut);

            (Model as ElPrimo).DashStarted += OnDashStarted;
            _dashAnimation.ValueChanged += (Model as ElPrimo).OnDashValueChanged;
            (Model as ElPrimo).DashValueChanged += OnDashValueChanged;
            _dashAnimation.AnimationEnd += (Model as ElPrimo).OnDashFinished;
            (Model as ElPrimo).DashFinished += OnDashFinished;
        }
    }
}