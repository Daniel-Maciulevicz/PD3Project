using UnityEngine;

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

            if (_loadingTimer <= 0)
            {
                FSM.CurrentState = FSM.ReadyState;
            }
        }

        public BrawlerPALoading(BrawlerPAFSM fsm) : base(fsm)
        { }
    }
}
