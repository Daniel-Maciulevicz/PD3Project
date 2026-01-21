
namespace PD3Stars.Models.FSM
{
    public class ColtHPFSM : BrawlerHPFSM
{
        public ColtHPFSM (Brawler context) : base(context)
        {
            RegeneratingState = new BrawlerHPRegeneratingState(this);
            CooldownState = new BrawlerHPCoolDownState(this);
            DeadState = new BrawlerHPDeadState(this);
            CurrentState = RegeneratingState;
        }
    }
}
