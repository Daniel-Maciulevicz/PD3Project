using PD3Stars.Presenters;
using System;
using UnityEngine;

public class ElPrimoAttackHitbox : MonoBehaviour, IPoolItem
{
    protected float _damage;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Reset()
    { }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brawler"))
        {
            other.GetComponent<BrawlerPresenter>().Model.Health -= _damage;
        }
    }

    private void Awake()
    {
        _damage = 250;
    }
}
