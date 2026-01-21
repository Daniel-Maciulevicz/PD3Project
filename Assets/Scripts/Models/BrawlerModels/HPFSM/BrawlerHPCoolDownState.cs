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
            if (_cooldownTimer <= 0)
                FSM.CurrentState = FSM.RegeneratingState;

            _cooldownTimer -= deltaTime;
        }

        public BrawlerHPCoolDownState(BrawlerHPFSM fsm) : base(fsm) { }
    }
}