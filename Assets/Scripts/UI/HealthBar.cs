using UnityEngine;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _imageTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        private Camera _camera; 
        private float _barSizeX;

        private void Awake()
        {
            _barSizeX = _imageTransform.sizeDelta.x;
            _camera = Camera.main;
        }
        
        public void ChangeHealthBar(int currentHealth, int maxHealth)
        {
            _canvasGroup.alpha = currentHealth > 0 ? 1 : 0;
            _imageTransform.sizeDelta = new Vector2(_barSizeX * currentHealth / maxHealth, _imageTransform.sizeDelta.y);
        }

        private void Update()
        {
            if (_canvasGroup.alpha > 0)
            {
                var direction = Vector3.Normalize(transform.position - _camera.transform.position);
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}