using UnityEngine;

namespace LanderDemo
{
    public class Background : MonoBehaviour
    {
        private Vector3 _bufferVector;

        [SerializeField]
        private float _parallaxFactor = 0.1f;

        private Renderer _renderer;

        [SerializeField]
        private float _zOffset = 30;

        public new Renderer renderer
        {
            get
            {
                if (_renderer == null) _renderer = GetComponent<Renderer>();
                return _renderer;
            }
        }

        // Use this for initialization
        private void Start()
        {
            _bufferVector = new Vector3();
        }

        // Update is called once per frame
        private void Update()
        {
            _bufferVector.x = -Camera.main.transform.position.x * (1.0f - _parallaxFactor);
            _bufferVector.y = -Camera.main.transform.position.y * (1.0f - _parallaxFactor);
            _bufferVector.z = _zOffset;

            renderer.material.SetVector("_UVOffset", _bufferVector);
        }
    }
}