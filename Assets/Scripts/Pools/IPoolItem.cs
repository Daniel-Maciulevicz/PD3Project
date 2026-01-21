using UnityEngine;

public interface IPoolItem
{
    public void Activate();
    public void Reset();
    public void Deactivate();
}