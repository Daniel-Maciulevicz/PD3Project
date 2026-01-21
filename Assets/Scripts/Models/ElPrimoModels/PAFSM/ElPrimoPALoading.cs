
namespace PD3Stars.Models.FSM
{
    public class ElPrimoPALoading : BrawlerPALoading
    {
        public ElPrimoPALoading(BrawlerPAFSM fsm) : base(fsm)
        {
            _loadingTime = 0.3f;
        }
    }
}
