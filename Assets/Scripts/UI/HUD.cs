using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class HUD : MonoBehaviour
{
    public int BrawlerCount { get; private set; }
    private int _maxBrawlers;

    [SerializeField]
    private TextMeshProUGUI[] _healthValue;
    [SerializeField]
    private TextMeshProUGUI[] _reloadValue;

    private IHUDElement[] _things;

    public void ShowStats(IHUDElement thing)
    {
        if (BrawlerCount >= _maxBrawlers)
            return;

        thing.HUDNumber = BrawlerCount;
        _things[BrawlerCount] = thing;
        _things[BrawlerCount].HealthProgressChanged += OnThingHealthChanged;
        _things[BrawlerCount].ReloadProgressChanged += OnThingReloadChanged;

        BrawlerCount++;
    }

    private void OnThingHealthChanged(object sender, HUDValueChangedArgs args)
    {
        _healthValue[(sender as IHUDElement).HUDNumber].text = MathF.Round(args.NewValue * 100).ToString();
    }
    private void OnThingReloadChanged(object sender, HUDValueChangedArgs args)
    {
        _reloadValue[(sender as IHUDElement).HUDNumber].text = MathF.Round(args.NewValue * 100).ToString();
    }

    public static HUD Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);

            _maxBrawlers = 3;

            _things = new IHUDElement[_maxBrawlers];
        }
        else
            Destroy(gameObject);
    }
}