
namespace PD3Stars.Models.FSM
{
    public class BrawlerPAExecuting : BrawlerPAState
    {
        public override void OnEnter()
        {
            FSM.Context.PrimaryAttackRequest();
            FSM.CurrentState = FSM.LoadingState;
        }

        public BrawlerPAExecuting(BrawlerPAFSM fsm) : base(fsm)
        { }
    }
}

