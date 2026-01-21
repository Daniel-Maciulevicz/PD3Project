
namespace PD3Stars.Models.FSM
{
    public class ColtPAFSM : BrawlerPAFSM
    {
        public ColtPAFSM(Brawler context) : base(context)
        {
            LoadingState = new ColtPALoading(this);
            ReadyState = new BrawlerPAReady(this);
            ExecutingState = new BrawlerPAExecuting(this);
            CooldownState = new BrawlerPACooldown(this);
            DeadState = new BrawlerPADead(this);
            CurrentState = ReadyState;
        }
    }
}
