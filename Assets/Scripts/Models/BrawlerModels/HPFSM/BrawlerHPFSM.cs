
namespace PD3Stars.Models.FSM
{
    public class BrawlerHPFSM : FiniteStateMachine
    {
        public Brawler Context { get; set; }

        public new BrawlerHPState CurrentState { get { return base.CurrentState as BrawlerHPState; } set { base.CurrentState = value; } }

        public BrawlerHPState RegeneratingState { get; set; }
        public BrawlerHPState CooldownState { get; set; }
        public BrawlerHPState DeadState { get; set; }

        public BrawlerHPFSM(Brawler context)
        {
            Context = context;
        }
    }
}