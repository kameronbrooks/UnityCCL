using UnityEngine;
using UnityEngine.UI;

namespace LanderDemo
{
    public class LanderDemo : MonoBehaviour
    {
        private static LanderDemo _instance;

        [SerializeField]
        private CodeModal _codeWindow;

        [SerializeField]
        private Text _console;

        [SerializeField]
        private ControlModule _controlModule;

        [SerializeField]
        private Modal _modal;

        public static LanderDemo instance
        {
            get
            {
                return _instance;
            }
        }

        public void Log(string text)
        {
            _console.text = text;
        }

        public void OpenCodeWindow()
        {
            _codeWindow.Open(_controlModule);
        }

        public void OpenModal(string message)
        {
            _modal.Open(message);
        }

        [RuntimeInitializeOnLoadMethod]
        private void Awake()
        {
            _instance = this;
            if (!CCL.Core.Assembly.main.isLoaded)
            {
                CCL.ScriptUtility.LoadStandardLibraries();
            }

            Physics2D.gravity = new Vector2(0, -1.62f);

            OpenModal(@"
MISSION:

LAND THE LANDER WITHOUT BLOWING UP. USE THE CODE MENU BUTTON TO EDIT THE CONTROL MODULE CODE, PRESS UPDATE TO SAVE CODE

PARAMETERS:
- MUST LAND WITH A SPEED LESS THAN 2 METERS PER SECOND
- LIMITED FUEL
- ONLY 3 THRUSTERS

");
            //Time.timeScale = 0.1f;
        }
    }
}