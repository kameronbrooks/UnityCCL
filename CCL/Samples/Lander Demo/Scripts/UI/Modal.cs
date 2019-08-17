using UnityEngine;
using UnityEngine.UI;

namespace LanderDemo
{
    public class Modal : MonoBehaviour
    {
        [SerializeField]
        private Text _messageText;

        public void Close()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Open(string message)
        {
            gameObject.SetActive(true);
            _messageText.text = message;
            Time.timeScale = 0.1f;
        }
    }
}