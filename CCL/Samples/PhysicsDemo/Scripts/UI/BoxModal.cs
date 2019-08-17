using UnityEngine;
using UnityEngine.UI;

namespace PhysicsDemo
{
    public class BoxModal : MonoBehaviour
    {
        [SerializeField]
        private InputField _onCollisionInput;

        [SerializeField]
        private InputField _onFixedUpdateInput;

        [SerializeField]
        private InputField _onLaserHitInput;

        private Box _target;

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open(Box box)
        {
            SetTarget(box);
            gameObject.SetActive(true);
        }

        public void SetTarget(Box box)
        {
            _target = box;
            _onLaserHitInput.text = box.OnLaserHitScript;
            _onCollisionInput.text = box.OnCollisionScript;
            _onFixedUpdateInput.text = box.OnFixedUpdateScript;
        }

        public void UpdateOnCollisionScript()
        {
            _target.SetOnCollisionScript(_onCollisionInput.text);
        }

        public void UpdateOnFixedUpdateScript()
        {
            _target.SetOnFixedUpdateScript(_onFixedUpdateInput.text);
        }

        public void UpdateOnLaserHitScript()
        {
            _target.SetOnLaserHitScript(_onLaserHitInput.text);
        }

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}