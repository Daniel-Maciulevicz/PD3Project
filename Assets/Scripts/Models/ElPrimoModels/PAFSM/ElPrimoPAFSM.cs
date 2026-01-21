
namespace PD3Stars.Models.FSM
{
    public class ElPrimoPAFSM : BrawlerPAFSM
    {
        public ElPrimoPAFSM(Brawler context) : base(context)
        {
            LoadingState = new ElPrimoPALoading(this);
            ReadyState = new BrawlerPAReady(this);
            ExecutingState = new BrawlerPAExecuting(this);
            CooldownState = new BrawlerPACooldown(this);
            DeadState = new BrawlerPADead(this);
            CurrentState = ReadyState;
        }
    }
}