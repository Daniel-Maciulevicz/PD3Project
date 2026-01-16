using System;
using UnityEngine;

public interface IStatusBar
{
    public event EventHandler StatusChanged;
    public float Progress { get; }
}