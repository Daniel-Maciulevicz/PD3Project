namespace PD3Stars.Models.FSM
{
    public class BrawlerHPCoolDownState : BrawlerHPState
    {
        private float _cooldownTime = 3;
        private float _cooldownTimer;

        public override void OnEnter()
        {
            _cooldownTimer = _cooldownTime;
        }

        public override void Update(float deltaTime)
        {
            _cooldownTimer -= deltaTime;

            FSM.CurrentState = FSM.RegeneratingState;
        }

        public BrawlerHPCoolDownState(BrawlerHPFSM fsm) : base(fsm) { }
    }
}