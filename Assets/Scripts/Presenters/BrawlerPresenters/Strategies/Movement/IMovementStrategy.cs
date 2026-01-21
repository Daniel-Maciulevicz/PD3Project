using UnityEngine;
using PD3Stars.Strategies;
using PD3Stars.Models;
using PD3Stars.Presenters;

namespace PD3Stars.Strategies
{
    public interface IMovementStrategy : IBrawlerStrategy
    {
        public Vector2 MoveInput { get; }
    }
}