using UnityEngine;
using UnityEngine.Events;

namespace LanderDemo
{
    [System.Serializable]
    public class Accelerometer : MonoBehaviour
    {
        private Vector2 _avgAcceleration;

        [SerializeField]
        private Rigidbody2D _body;

        private Vector2 _lastVel;

        [SerializeField]
        private AccelerometerEvent _onFixedUpdate;

        public Vector2 acceleration
        {
            get
            {
                return _avgAcceleration;
            }
        }

        public float angularVelocity
        {
            get
            {
                return _body.angularVelocity;
            }
        }

        public float speed
        {
            get
            {
                return velocity.magnitude;
            }
        }

        public Vector2 velocity
        {
            get
            {
                return _body.velocity;
            }
        }

        private void FixedUpdate()
        {
            _avgAcceleration = (velocity - _lastVel) / Time.fixedDeltaTime;
            _lastVel = velocity;
            _onFixedUpdate?.Invoke(this);
        }

        [System.Serializable]
        public class AccelerometerEvent : UnityEvent<Accelerometer> { }
    }
}