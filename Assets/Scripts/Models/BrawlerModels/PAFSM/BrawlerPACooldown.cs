
namespace PD3Stars.Models.FSM
{
    public class BrawlerPACooldown : BrawlerPAState
    {
        protected float _cooldownTime = 3;
        protected float _cooldownTimer;

        public override void OnEnter()
        {
            _cooldownTimer = _cooldownTime;
        }

        public override void Update(float deltaTime)
        {
            _cooldownTimer -= deltaTime;

            FSM.CurrentState = FSM.LoadingState;
        }

        public BrawlerPACooldown(BrawlerPAFSM fsm) : base(fsm)
        { }
    }
}
