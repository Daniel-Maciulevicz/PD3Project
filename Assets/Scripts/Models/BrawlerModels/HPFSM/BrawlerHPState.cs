namespace PD3Stars.Models.FSM
{
    public abstract class BrawlerHPState : IState
    {
        protected BrawlerHPFSM FSM { get; set; }

        public virtual void OnEnter()
        { }
        public virtual void OnExit()
        { }
        public virtual void Update(float deltaTime)
        { }

        public BrawlerHPState(BrawlerHPFSM fsm)
        {
            FSM = fsm;
        }
    }
}