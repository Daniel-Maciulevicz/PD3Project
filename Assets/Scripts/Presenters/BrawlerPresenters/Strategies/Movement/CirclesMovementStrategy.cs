using PD3Stars.Models;
using PD3Stars.Presenters;
using UnityEngine;

namespace PD3Stars.Strategies
{
    public class CirclesMovementStrategy : MovementStrategyBaseClass
    {
        protected float _radius;

        protected float _fixedTime;

        public override void FixedUpdate(float fixedDeltaTime)
        {
            _fixedTime += fixedDeltaTime;

            MoveInput = new Vector2(_radius * Mathf.Sin(_fixedTime), _radius * Mathf.Cos(_fixedTime));
        }

        public CirclesMovementStrategy(Brawler context, BrawlerPresenter presenterContext) : base (context, presenterContext) 
        {
            _radius = 0.35f;
        }
    }
}