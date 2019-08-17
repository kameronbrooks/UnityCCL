using System.Collections.Generic;
using UnityEngine;

namespace LanderDemo
{
    public class Ground : MonoBehaviour
    {
        [SerializeField]
        private List<GroundChunk> _chunks;

        private float _chunkSize = 30;

        [SerializeField]
        private Transform _focusTransform;

        private Stack<GroundChunk> _waitingChunks;

        public void UpdateChunks(Vector3 focus)
        {
            float focusX = focus.x;
            int focusIndex = Mathf.FloorToInt(focusX / _chunkSize);
            GroundChunk[] focusChunks = new GroundChunk[3];
            if (_waitingChunks == null) _waitingChunks = new Stack<GroundChunk>();

            for (int i = 0; i < _chunks.Count; i += 1)
            {
                if (_chunks[i].xIndex == focusIndex)
                {
                    focusChunks[1] = _chunks[i];
                }
                else if (_chunks[i].xIndex == focusIndex - 1)
                {
                    focusChunks[0] = _chunks[i];
                }
                else if (_chunks[i].xIndex == focusIndex + 1)
                {
                    focusChunks[2] = _chunks[i];
                }
                else
                {
                    _waitingChunks.Push(_chunks[i]);
                }
            }
            for (int i = 0; i < focusChunks.Length; i += 1)
            {
                if (focusChunks[i] == null)
                {
                    focusChunks[i] = _waitingChunks.Pop();
                    focusChunks[i].MoveTo((float)focusIndex * _chunkSize + _chunkSize * ((float)i - 1));
                }
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            UpdateChunks(_focusTransform.position);
        }

        // Use this for initialization
        private void Start()
        {
        }
    }
}