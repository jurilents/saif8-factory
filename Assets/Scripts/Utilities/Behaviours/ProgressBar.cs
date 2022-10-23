using UnityEngine;

namespace Utilities.Behaviours
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class ProgressBar : MonoBehaviour
    {
        public RectTransform barMask;

        [Range(0, 1)] public float value = 1;

        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            barMask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value * _rectTransform.rect.width);
        }
    }
}