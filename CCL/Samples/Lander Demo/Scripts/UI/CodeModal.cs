using UnityEngine;

namespace LanderDemo
{
    public class CodeModal : MonoBehaviour
    {
        [SerializeField]
        private CCL.UI.CodeField _input;

        private IScriptableFixedUpdate _scriptable;

        public void Close()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Open(IScriptableFixedUpdate scriptable)
        {
            _input.contextType = typeof(ControlModule);
            gameObject.SetActive(true);
            _scriptable = scriptable;
            _input.text = _scriptable.fixedUpdateScript;
            Time.timeScale = 0.1f;
        }

        public void SetScript()
        {
            _scriptable.fixedUpdateScript = _input.text;
        }
    }
}