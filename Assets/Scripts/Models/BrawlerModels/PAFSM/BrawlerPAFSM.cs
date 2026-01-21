
namespace PD3Stars.Models.FSM
{
    public class BrawlerPAFSM : FiniteStateMachine
    {
        public Brawler Context { get; set; }

        public new BrawlerPAState CurrentState { get { return base.CurrentState as BrawlerPAState; } set { base.CurrentState = value; } }

        public BrawlerPAState LoadingState { get; set; }
        public BrawlerPAState ReadyState { get; set; }
        public BrawlerPAState ExecutingState { get; set; }
        public BrawlerPAState CooldownState { get; set; }
        public BrawlerPAState DeadState { get; set; }

        public BrawlerPAFSM(Brawler context)
        {
            Context = context;
        }
    }
}