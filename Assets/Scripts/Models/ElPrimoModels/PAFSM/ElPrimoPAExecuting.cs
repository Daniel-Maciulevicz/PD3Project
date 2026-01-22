
namespace PD3Stars.Models.FSM
{
    public class ElPrimoPAExecuting : BrawlerPAState
    {
        public override void OnEnter()
        {
            FSM.Context.PrimaryAttackRequest();
        }

        public ElPrimoPAExecuting(BrawlerPAFSM fsm) : base(fsm)
        { }
    }
}

