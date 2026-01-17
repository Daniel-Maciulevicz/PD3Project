using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace PD3Stars.UI
{
    public class HealthBarPresenter 
    {
        const float _width = 65f;
        const float _height = 8f;

        public IHealthBar Model { get; }

        public VisualElement HealthBarClone { get; set; }

        private readonly Transform _hbTransform;
        private readonly UIDocument _hudDocument;
        private VisualElement _blackSide;

        private void ModelHealthChanged(object sender, EventArgs e) 
        {
            _blackSide.style.width = new StyleLength(new Length( 100 - Model.HealthProgress * 100,LengthUnit.Percent));
        }
        public void UpdatePosition() 
        {
            float dist = Vector3.Distance(Camera.main.transform.position, _hbTransform.transform.position);
            float distScale = 1f / Mathf.Clamp01(dist / 20f);

            Vector2 screenpos = RuntimePanelUtils.CameraTransformWorldToPanel(_hudDocument.runtimePanel, _hbTransform.position, Camera.main);

            HealthBarClone.style.top = screenpos.y;  // -_height * distScale;
            HealthBarClone.style.left = screenpos.x - _width * distScale / 2;
            HealthBarClone.style.width = _width * distScale;
            HealthBarClone.style.height = _height * distScale;
        }

        public HealthBarPresenter(IHealthBar healthPublisher, Transform healthBarTransform, VisualElement healthBarClone, UIDocument hudDocument)
        {
            Model = healthPublisher;
            Model.HealthChanged += ModelHealthChanged;

            _hbTransform = healthBarTransform;
            HealthBarClone = healthBarClone;
            _hudDocument = hudDocument;
            _hudDocument.rootVisualElement.Add(healthBarClone);
            _blackSide = healthBarClone.Q("BlackSide");
            _blackSide.style.width = new StyleLength(new Length(100 - Model.HealthProgress * 100, LengthUnit.Percent));

            healthBarClone.style.position = Position.Absolute;
            healthBarClone.style.width = _width;
            healthBarClone.style.height = _height / 2;
        }
    }
}