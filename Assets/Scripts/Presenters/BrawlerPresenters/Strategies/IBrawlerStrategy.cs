using PD3Stars.Models;
using PD3Stars.Presenters;

namespace PD3Stars.Strategies
{
    public interface IBrawlerStrategy
    {
        public Brawler Context { get; }
        public BrawlerPresenter PresenterContext { get; }
        public void Update(float deltaTime);
        public void FixedUpdate(float fixedDeltaTime);
    }
}