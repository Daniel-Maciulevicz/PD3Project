using PD3Stars.Models.FSM;
using System;
using PD3Animations;

namespace PD3Stars.Models
{
    public class ElPrimo : Brawler
    {
        public float DashDistance { get; private set; }
        public float DashTime { get; private set; }

        public event EventHandler DashStarted;
        public event EventHandler<float> DashValueChanged;
        public event EventHandler DashFinished;

        public override void PrimaryAttackRequest()
        {
            DashStarted.Invoke(this, EventArgs.Empty);
        }
        public void OnDashValueChanged(object sender, ValueChangedArgs<float> args) 
        {
            if ((args.NewValue - args.OldValue) <= 0)
                return;

            DashValueChanged.Invoke(this, args.NewValue - args.OldValue);
        }
        public void OnDashFinished(object sender, EventArgs args)
        {
            PAFSM.CurrentState = PAFSM.LoadingState;

            DashFinished.Invoke(this, EventArgs.Empty);
        }

        public ElPrimo()
        {
            HPFSM = new ElPrimoHPFSM(this);
            PAFSM = new ElPrimoPAFSM(this);

            DashDistance = 5;
            DashTime = 1;
        }
    }
}