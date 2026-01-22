using PD3Stars.UI;
using System;
using UnityEngine;

public class HealthBarCanvasPresenter : MonoBehaviour
{
    public IHealthBar Model
    {
        get { return _model; }
        set
        {
            IHealthBar previousModel = null;

            if (_model == value)
                return;
            if (_model != null)
            {
                _model.HealthChanged -= OnHealthChanged;
                previousModel = _model;
            }

            _model = value;
            _model.HealthChanged += OnHealthChanged;
        }
    }
    private IHealthBar _model;

    [SerializeField]
    private RectTransform _healthBar;

    public void UpdatePosition()
    {
        transform.LookAt(Camera.main.transform);
    }
    private void OnHealthChanged(object sender, EventArgs args)
    {
        _healthBar.localScale = new Vector2(Model.HealthProgress, 1);
    }
}
