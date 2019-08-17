using UnityEngine;

namespace PhysicsDemo
{
    public class SpaceSceneContext : MonoBehaviour
    {
        [SerializeField]
        private GameObject _boxPrefab;

        public Box CreateBox()
        {
            return Box.Build(_boxPrefab);
        }

        public Box[] GetAllBoxes()
        {
            return FindObjectsOfType<Box>();
        }

        public Box GetBoxById(int id)
        {
            Box[] boxes = FindObjectsOfType<Box>();
            for (int i = 0; i < boxes.Length; i += 1)
            {
                if (boxes[i].id == id) return boxes[i];
            }
            return null;
        }

        public void Log(int str)
        {
            Debug.Log("int: " + str.ToString());
        }

        public void Log(string str)
        {
            Debug.Log("string: " + str.ToString());
        }
    }
}