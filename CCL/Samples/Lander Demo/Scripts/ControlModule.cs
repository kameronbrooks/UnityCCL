using UnityEngine;

namespace LanderDemo
{
    public class ControlModule : MonoBehaviour, IControlModule, IScriptableFixedUpdate
    {
        [SerializeField]
        private Accelerometer _accelerometer;

        [SerializeField]
        private Thruster _centerThruster;

        private int _controlMode = 0;

        [SerializeField]
        [Multiline]
        private string _fixedUpdateScript;

        private bool _isUnlocked = false;

        [SerializeField]
        private Thruster _leftThruster;

        private CCL.Core.CompiledScript _onFixedUpdate;
        private string _overrideCode = "**PINTSIZEDSLASHER**";

        [SerializeField]
        private RangeFinder _rangeFinder;

        [SerializeField]
        private Thruster _rightThruster;

        [SerializeField]
        private Sensor[] _sensors;

        public Accelerometer accelerometer
        {
            get
            {
                return _accelerometer;
            }
        }

        public Thruster centerThruster
        {
            get
            {
                return _centerThruster;
            }
        }

        public string fixedUpdateScript
        {
            get
            {
                return _fixedUpdateScript;
            }
            set
            {
                try
                {
                    _fixedUpdateScript = value;
                    _onFixedUpdate = CCL.ScriptUtility.CompileScript(_fixedUpdateScript, typeof(IControlModule));
                    _onFixedUpdate.SetContext((IControlModule)this);
                }
                catch (System.Exception e)
                {
                    LanderDemo.instance.OpenModal(e.ToString());
                }
            }
        }

        public Thruster leftThruster
        {
            get
            {
                return _leftThruster;
            }
        }

        public RangeFinder rangeFinder
        {
            get
            {
                return _rangeFinder;
            }
        }

        public Thruster rightThruster
        {
            get
            {
                return _rightThruster;
            }
        }

        public bool Override(string overrideCode)
        {
            if (_overrideCode == overrideCode)
            {
                _isUnlocked = false;
                return true;
            }
            return false;
        }

        public void SetControlMode(int mode)
        {
            if (_isUnlocked)
            {
                _controlMode = mode;
            }
        }

        private void ControlManual()
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rightThruster.SetThrottle(1.0f);
            }
            else
            {
                _rightThruster.SetThrottle(0.0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _leftThruster.SetThrottle(1.0f);
            }
            else
            {
                _leftThruster.SetThrottle(0.0f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                _centerThruster.SetThrottle(1.0f);
            }
            else
            {
                _centerThruster.SetThrottle(0.0f);
            }
        }

        private void FixedUpdate()
        {
            if (_onFixedUpdate != null)
            {
                object res = _onFixedUpdate.Invoke();
                if (res != null)
                {
                    LanderDemo.instance.Log(res.ToString());
                }
            }
        }

        // Use this for initialization
        private void Start()
        {
            if (_fixedUpdateScript != "")
            {
                fixedUpdateScript = _fixedUpdateScript;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (_controlMode == 0)
            {
                ControlManual();
            }
        }
    }
}