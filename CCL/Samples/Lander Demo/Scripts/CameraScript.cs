using UnityEngine;

namespace LanderDemo
{
    public class CameraScript : MonoBehaviour
    {
        private Vector3 _focus;

        [SerializeField]
        private Vector3 _offset = new Vector3(0, 0, -10);

        [SerializeField]
        private float _speed = 15f;

        [SerializeField]
        private Transform _targetTransform;

        public void Start()
        {
        }

        private void LateUpdate()
        {
            _focus = Vector3.MoveTowards(_focus, _targetTransform.position, Time.deltaTime * Vector3.Distance(_targetTransform.position, _focus) * _speed);
            _focus.z = 0;
            transform.position = _focus + _offset;
        }
    }
}