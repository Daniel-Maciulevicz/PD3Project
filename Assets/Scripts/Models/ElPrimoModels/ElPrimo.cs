using PD3Stars.Models.FSM;

namespace PD3Stars.Models
{
    public class ElPrimo : Brawler
    {
        public override void PrimaryAttackRequest()
        {
            
        }

        public ElPrimo()
        {
            HPFSM = new ElPrimoHPFSM(this);
            PAFSM = new ElPrimoPAFSM(this);
        }
    }
}