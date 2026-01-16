namespace PD3Stars.Models.FSM
{
    public class BrawlerHPRegeneratingState : BrawlerHPState
    {
        public override void Update(float deltaTime)
        {
            FSM.Context.HPRegenerate(deltaTime);
        }

        public BrawlerHPRegeneratingState(BrawlerHPFSM fsm) : base(fsm) { }
    }
}