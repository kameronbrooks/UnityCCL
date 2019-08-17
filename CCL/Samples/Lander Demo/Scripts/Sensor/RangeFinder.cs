using UnityEngine;
using UnityEngine.Events;

namespace LanderDemo
{
    public class RangeFinder : MonoBehaviour
    {
        [SerializeField]
        private float _distance;

        [SerializeField]
        private RangeFinderEvent _onUpdate;

        private float _range;

        public float range
        {
            get
            {
                return _range;
            }
        }

        private void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _distance, 1 << LayerMask.NameToLayer("Ground"));
            if (hit.collider != null)
            {
                _range = hit.distance;
            }
            else
            {
                _range = float.PositiveInfinity;
            }
            _onUpdate?.Invoke(this);
        }

        [System.Serializable]
        public class RangeFinderEvent : UnityEvent<RangeFinder> { }
    }
}