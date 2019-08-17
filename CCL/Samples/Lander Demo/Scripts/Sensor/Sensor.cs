using UnityEngine;

namespace LanderDemo
{
    public class Sensor : MonoBehaviour
    {
        public virtual string sensorName
        {
            get
            {
                return "Sensor";
            }
        }

        public virtual float GetData(string id = "")
        {
            return 0f;
        }

        public virtual string[] GetOutputIDs()
        {
            return null;
        }
    }
}