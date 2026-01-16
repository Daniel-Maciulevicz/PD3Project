using UnityEngine;

namespace PD3Stars.Models.FSM
{
    public class ColtPALoading : BrawlerPALoading
    {
        public ColtPALoading(BrawlerPAFSM fsm) : base(fsm)
        {
            _loadingTime = 0.15f;
        }
    }
}
