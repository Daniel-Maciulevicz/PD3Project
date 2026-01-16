using UnityEngine;

namespace PD3Stars.Models.FSM
{
    public class BrawlerPAReady : BrawlerPAState
    {
        public override void PrimaryAttackRequest()
        {
            FSM.CurrentState = FSM.ExecutingState;
        }

        public BrawlerPAReady(BrawlerPAFSM fsm) : base(fsm)
        { }
    }
}