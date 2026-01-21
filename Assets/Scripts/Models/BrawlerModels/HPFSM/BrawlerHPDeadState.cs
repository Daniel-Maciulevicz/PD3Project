namespace PD3Stars.Models.FSM
{
    public class BrawlerHPDeadState : BrawlerHPState
    {
        private float _waitTime;
        private float _waitTimer;

        public override void OnEnter()
        {
            FSM.Context.CanMove = false;

            _waitTimer = _waitTime;
        }
        public override void Update(float deltaTime)
        {
            if (_waitTimer <= 0)
                FSM.Context.RespawnRequest();

            _waitTimer -= deltaTime;
        }
        public override void OnExit()
        {
            FSM.Context.CanMove = true;
        }

        public BrawlerHPDeadState(BrawlerHPFSM fsm) : base(fsm) 
        {
            _waitTime = 3;
        }
    }
}