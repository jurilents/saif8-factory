using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Behaviours
{
    [RequireComponent(typeof(Text))]
    public class FPSCounter : MonoBehaviour
    {
        public float timeBetweenUpdates = 1f;

        private Text _text;
        private DateTime _timeOfLastCount;
        private float _leftover;
        private float _frameCount;

        private void Start()
        {
            _text = GetComponent<Text>();
            _timeOfLastCount = DateTime.Now;
            _leftover = 0;
        }

        public void Update()
        {
            _frameCount++;
            float ms = (float) (DateTime.Now - _timeOfLastCount).TotalSeconds + _leftover;
            if (ms <= timeBetweenUpdates) return;

            _leftover = ms - timeBetweenUpdates;
            _timeOfLastCount = DateTime.Now;
            float fps = _frameCount * (1f / timeBetweenUpdates);
            _text.text = fps.ToString("00");
            _frameCount = 0;
        }
    }
}