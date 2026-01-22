using UnityEngine;

namespace PD3Stars.Presenters
{
    public class HUDElementPresenter
    {
        public IHUDElement Model { get; private set; }

        public HUDElementPresenter(IHUDElement hud)
        {
            Model = hud;

            HUD.Instance.ShowStats(Model);
        }
    }
}