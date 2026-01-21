using PD3Stars.Models;
using PD3Stars.Presenters;
using System;

namespace PD3Stars.Strategies
{
    public class ASAPAttackStrategy : AttackStrategyBaseClass
    {
        public override event EventHandler AttackStarted;

        public override void FixedUpdate(float fixedDeltaTime)
        {
            if (Context.PAFSM.CurrentState == Context.PAFSM.ReadyState)
            {
                AttackStarted.Invoke(this, EventArgs.Empty);
            }
        }

        public ASAPAttackStrategy(Brawler context, BrawlerPresenter presenterContext) : base(context, presenterContext) { }
    }
}