using System;
using UnityEngine;
using PD3Stars.Models.FSM;

namespace PD3Stars.Models
{
    public class Colt : Brawler
    {
        public event EventHandler<ColtBulletCreatedArgs> ColtBulletCreated;

        public override void FixedUpdate(float fixedDeltaTime)
        {
            base.FixedUpdate(fixedDeltaTime);
        }

        public override void PrimaryAttackRequest()
        {
            ColtBulletCreated(this, new ColtBulletCreatedArgs(new ColtBullet()));
        }

        public Colt()
        {
            HPFSM = new ColtHPFSM(this);
            PAFSM = new ColtPAFSM(this);
        }
    }
}