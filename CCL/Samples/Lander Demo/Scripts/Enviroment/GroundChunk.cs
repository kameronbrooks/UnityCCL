using UnityEngine;

namespace LanderDemo
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class GroundChunk : MonoBehaviour
    {
        public int xIndex;

        [SerializeField]
        private float _bottomPadding = 10;

        private PolygonCollider2D _collider;
        private Vector2[] _colliderPoints;
        private MeshFilter _filter;

        [SerializeField]
        private float _lacunarity = 1.1f;

        private Mesh _mesh;

        [SerializeField]
        private float _noiseOffset = 0.0f;

        [SerializeField]
        private float _noiseScale = 1.0f;

        [SerializeField]
        private int _octaves = 4;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _patternVariation = 0.5f;

        [SerializeField]
        private float _persistance = 0.1f;

        private MeshRenderer _renderer;

        [SerializeField]
        private int _resolution = 8;

        private int[] _triangles;
        private Vector3[] _vertices;

        [SerializeField]
        private float _worldHeight = 10;

        [SerializeField]
        private float _worldWidth = 10;

        public new PolygonCollider2D collider
        {
            get
            {
                if (_collider == null) _collider = GetComponent<PolygonCollider2D>();
                return _collider;
            }
        }

        public MeshFilter filter
        {
            get
            {
                if (_filter == null) _filter = GetComponent<MeshFilter>();
                return _filter;
            }
        }

        public float worldWidth
        {
            get
            {
                return _worldWidth;
            }
        }

        public void Clear()
        {
            _vertices = null;
            _colliderPoints = null;
            _triangles = null;
        }

        public float GetHeight(float x)
        {
            float noiseVal = 0;

            float amplitude = 1;
            float frequency = 1;
            for (int i = 0; i < _octaves; i += 1)
            {
                float wx = (x < 0) ? (1 / _noiseScale * frequency) + x : x;

                float perlinVal = Mathf.PerlinNoise((wx + _noiseOffset) / _noiseScale * frequency, _patternVariation) * 2 - 1;
                noiseVal += perlinVal * amplitude * _worldHeight;

                amplitude *= _persistance;
                frequency *= _lacunarity;
            }

            return noiseVal;
        }

        public void MoveTo(float x)
        {
            Vector3 vec = transform.position;
            vec.x = x;
            transform.position = vec;
            UpdateMesh();
            xIndex = Mathf.FloorToInt(transform.position.x / _worldWidth);
        }

        public void OnValidate()
        {
            Clear();
            UpdateMesh();
            xIndex = Mathf.FloorToInt(transform.position.x / _worldWidth);
        }

        public void UpdateMesh()
        {
            if (_mesh == null) _mesh = new Mesh();
            GeneratePoints();

            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;

            _mesh.RecalculateBounds();
            filter.mesh = _mesh;
            collider.SetPath(0, _colliderPoints);
        }

        private void GeneratePoints()
        {
            if (_vertices == null || _vertices.Length != _resolution * 2)
            {
                _vertices = new Vector3[_resolution * 2];
                _colliderPoints = new Vector2[_resolution * 2];
                _triangles = new int[(_resolution * 2 - 2) * 3];
            }

            float start = -(_worldWidth / 2);
            float step = _worldWidth / (_resolution - 1);
            for (int i = 0; i < _resolution; i += 1)
            {
                float localX = start + step * i;
                Vector3 surfacePoint = new Vector3(localX, GetHeight(transform.position.x + localX), 0);
                Vector3 bottomPoint = new Vector3(localX, -_bottomPadding, 0);
                _vertices[i] = surfacePoint;
                _vertices[_vertices.Length - 1 - i] = bottomPoint;

                _colliderPoints[i] = surfacePoint;
                _colliderPoints[_vertices.Length - 1 - i] = bottomPoint;
            }

            int triIndex = 0;
            for (int i = 0; i < _resolution - 1; i += 1)
            {
                _triangles[triIndex + 2] = _vertices.Length - 1 - i;
                _triangles[triIndex + 1] = _vertices.Length - 1 - (i + 1);
                _triangles[triIndex + 0] = i;
                _triangles[triIndex + 5] = i;
                _triangles[triIndex + 4] = _vertices.Length - 1 - (i + 1);
                _triangles[triIndex + 3] = i + 1;
                triIndex += 6;
            }
        }
    }
}