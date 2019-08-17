using UnityEngine;
using UnityEngine.UI;

namespace PhysicsDemo
{
    public class Modal : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public void Close()
        {
            this.gameObject.SetActive(false);
        }

        public void ShowMessage(string message)
        {
            this.gameObject.SetActive(true);
            _text.text = message;
        }
    }
}