using PD3Stars.Models.FSM;

namespace PD3Stars.Models
{
    public class Colt : Brawler
    {
        public ColtBulletPool BulletPool { get; private set; }

        public override void PrimaryAttackRequest()
        {
            BulletPool.ActivateNextItem();
        }

        public Colt()
        {
            HPFSM = new ColtHPFSM(this);
            PAFSM = new ColtPAFSM(this);
            BulletPool = new ColtBulletPool();
        }
    }
}