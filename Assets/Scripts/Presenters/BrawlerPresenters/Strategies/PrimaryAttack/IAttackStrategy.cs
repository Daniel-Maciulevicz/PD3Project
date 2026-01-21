using UnityEngine;
using PD3Stars.Strategies;
using PD3Stars.Models;
using PD3Stars.Presenters;
using System;

namespace PD3Stars.Strategies
{
    public interface IAttackStrategy : IBrawlerStrategy
    {
        public event EventHandler AttackStarted;
    }
}