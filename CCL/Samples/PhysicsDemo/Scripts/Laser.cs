using System.Collections.Generic;
using UnityEngine;

namespace PhysicsDemo
{
    [ExecuteInEditMode]
    public class Laser : MonoBehaviour
    {
        [SerializeField]
        private Color _color;

        [SerializeField]
        private LineRenderer _lineRenderer;

        private float accum = 0;

        [SerializeField]
        private int maxBounces;

        public Color color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
                _lineRenderer.startColor = _color;
                _lineRenderer.endColor = _color;
            }
        }

        public LineRenderer lineRenderer
        {
            get
            {
                if (_lineRenderer == null) _lineRenderer = GetComponent<LineRenderer>();
                return _lineRenderer;
            }
        }

        private void FixedUpdate()
        {
        }

        private void HitCollider(RaycastHit2D hit)
        {
            LaserHit laserHit = new LaserHit()
            {
                hit = hit,
                laser = this
            };

            hit.collider.SendMessage("OnLaserHit", laserHit, SendMessageOptions.DontRequireReceiver);
        }

        private void OnValidate()
        {
            _lineRenderer.startColor = _color;
            _lineRenderer.endColor = _color;
        }

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            accum += Time.deltaTime;
            if (accum >= 0.0001f)
            {
                UpdateBeam();
                accum -= 0.0001f;
            }
        }

        private void UpdateBeam()
        {
            RaycastHit2D hit;
            Vector2 origin = transform.position;
            Vector2 vector = transform.up;
            List<Vector3> _points = new List<Vector3>();
            _points.Add(origin);
            int curBounces = 0;
            do
            {
                hit = Physics2D.Raycast(origin, vector);
                if (hit.collider != null)
                {
                    _points.Add(hit.point);
                    vector = Vector3.Reflect(vector, hit.normal);
                    origin = hit.point + vector * 0.01f;
                    HitCollider(hit);
                }
                curBounces += 1;
            } while (hit.collider != null && curBounces < maxBounces);

            _points.Add(origin + vector * 10);
            lineRenderer.positionCount = _points.Count;
            lineRenderer.SetPositions(_points.ToArray());
        }
    }
}