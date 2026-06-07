using UnityEngine;
using System.Collections;


namespace Dust
{
    public class PlayerController : MonoBehaviour
    {
        public Oscillator refOscillator;
        public float rotationAmplitude;

        Camera _cam;
        float _minRotation;
        float _maxRotation;

        private Touch tempTouchs;
        private Vector3 touchedPos;
        private bool touchOn;

        private Ray ray;
        private RaycastHit hit;

        void Start()
        {
            _cam = Camera.main;
            _minRotation = _cam.transform.eulerAngles.y + rotationAmplitude;
            _maxRotation = _cam.transform.eulerAngles.y - rotationAmplitude;
        }

        void Update()
        {
            var vec = new Vector3(_cam.transform.eulerAngles.x, Mathf.LerpAngle(_minRotation, _maxRotation, refOscillator.value), _cam.transform.eulerAngles.z);
            _cam.transform.eulerAngles = Vector3.Lerp(_cam.transform.eulerAngles, vec, Time.deltaTime);
        }

    }
}