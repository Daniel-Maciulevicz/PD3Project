using System;

namespace PD3Stars.Models.FSM
{
    public class BrawlerPALoading : BrawlerPAState
    {
        protected float _loadingTime = 0;
        protected float _loadingTimer;

        public override void OnEnter()
        {
            _loadingTimer = _loadingTime;
        }

        public override void Update(float deltaTime)
        {
            _loadingTimer -= deltaTime;

            MathF.Max(_loadingTimer, 0);

            if (_loadingTimer <= 0)
            {
                FSM.CurrentState = FSM.ReadyState;
            }

            FSM.Context.ReloadProgress = 1 - _loadingTimer / _loadingTime;
        }

        public BrawlerPALoading(BrawlerPAFSM fsm) : base(fsm)
        { }
    }
}
