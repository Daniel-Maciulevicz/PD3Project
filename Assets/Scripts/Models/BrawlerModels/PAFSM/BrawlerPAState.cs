
namespace PD3Stars.Models.FSM
{
    public abstract class BrawlerPAState : IState
    {
        protected BrawlerPAFSM FSM { get; set; }

        public virtual void OnEnter()
        { }

        public virtual void OnExit()
        { }

        public virtual void Update(float deltaTime)
        { }

        public virtual void PrimaryAttackRequest()
        { }

        public BrawlerPAState(BrawlerPAFSM fsm)
        {
            FSM = fsm;
        }
    }
}
