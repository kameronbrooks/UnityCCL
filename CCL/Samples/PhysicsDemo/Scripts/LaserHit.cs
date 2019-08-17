using UnityEngine;

namespace PhysicsDemo
{
    public class LaserHit
    {
        public RaycastHit2D hit;
        public Laser laser;

        public Collider2D self
        {
            get
            {
                return hit.collider;
            }
        }
    }
}