using CCL.Core;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsDemo
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Box : MonoBehaviour
    {
        public static int field = 10;
        private static int _baseID = 1;

        private Rigidbody2D _body;

        private BoxCollider2D _collider;

        private Collision2D _collision;

        private int _id;

        private LaserHit _laserHit;

        private Dictionary<string, object> _metaData;

        private CompiledScript _onCollision;

        [SerializeField]
        [Multiline]
        private string _onCollisionScript;

        private CompiledScript _onFixedUpdate;

        [SerializeField]
        [Multiline]
        private string _onFixedUpdateScript;

        private CompiledScript _onLaserHit;

        [SerializeField]
        [Multiline]
        private string _onLaserHitScript;

        private SpriteRenderer _renderer;

        private List<string> _tags;

        public static int baseID
        {
            get
            {
                return _baseID;
            }
        }

        public Rigidbody2D body
        {
            get
            {
                if (_body == null) _body = GetComponent<Rigidbody2D>();
                return _body;
            }
        }

        public new BoxCollider2D collider
        {
            get
            {
                if (_collider == null) _collider = GetComponent<BoxCollider2D>();
                return _collider;
            }
        }

        public Collision2D collision
        {
            get
            {
                return _collision;
            }
        }

        public int id
        {
            get
            {
                return _id;
            }
        }

        public LaserHit laserHit
        {
            get
            {
                return _laserHit;
            }
        }

        public string OnCollisionScript
        {
            get
            {
                return _onCollisionScript;
            }
        }

        public string OnFixedUpdateScript
        {
            get
            {
                return _onFixedUpdateScript;
            }
        }

        public string OnLaserHitScript
        {
            get
            {
                return _onLaserHitScript;
            }
        }

        public new SpriteRenderer renderer
        {
            get
            {
                if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();
                return _renderer;
            }
        }

        public Box self
        {
            get
            {
                return this;
            }
        }

        public List<string> tags
        {
            get
            {
                return _tags;
            }

            set
            {
                _tags = value;
            }
        }

        public static Box Build(GameObject prefab)
        {
            GameObject ob = Instantiate(prefab);
            Box output = ob.GetComponent<Box>();
            output._id = _baseID += 1;

            return output;
        }

        public void FixedUpdate()
        {
            Vector3 pos = transform.position;
            Vector3 min = Camera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 max = Camera.main.ViewportToWorldPoint(Vector3.one);

            if (pos.x > max.x) pos.x = min.x + 0.01f;
            if (pos.x < min.x) pos.x = max.x - 0.01f;

            if (pos.y > max.y) pos.y = min.y + 0.01f;
            if (pos.y < min.y) pos.y = max.y - 0.01f;

            transform.position = pos;
            if (_onFixedUpdate != null)
            {
                try
                {
                    _onFixedUpdate.Invoke();
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            }
        }

        public object GetData(string name)
        {
            object val = null;
            _metaData.TryGetValue(name, out val);
            return val;
        }

        public void OnLaserHit(LaserHit laserHit)
        {
            _laserHit = laserHit;
            if (_onLaserHit != null)
            {
                try
                {
                    _onLaserHit.Invoke();
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            }
            _laserHit = null;
        }

        public void SetData(string name, object value)
        {
            if (_metaData.ContainsKey(name))
            {
                _metaData[name] = value;
            }
            else
            {
                _metaData.Add(name, value);
            }
        }

        public void SetOnCollisionScript(string code)
        {
            try
            {
                _onCollisionScript = code;
                _onCollision = CCL.ScriptUtility.CompileScript(code, typeof(Box));
                _onCollision.SetContext(this);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                _onCollision = null;
            }
        }

        public void SetOnFixedUpdateScript(string code)
        {
            try
            {
                _onFixedUpdateScript = code;
                _onFixedUpdate = CCL.ScriptUtility.CompileScript(code, typeof(Box));
                _onFixedUpdate.SetContext(this);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                _onFixedUpdate = null;
            }
        }

        public void SetOnLaserHitScript(string code)
        {
            try
            {
                _onLaserHitScript = code;
                _onLaserHit = CCL.ScriptUtility.CompileScript(code, typeof(Box));
                _onLaserHit.SetContext(this);
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                _onLaserHit = null;
            }
        }

        public void Start()
        {
            if (!CCL.Core.Assembly.main.isLoaded)
            {
                CCL.ScriptUtility.LoadStandardLibraries();
            }
            _metaData = new Dictionary<string, object>();
            _tags = new List<string>();

            if (_onFixedUpdateScript != "")
            {
                SetOnFixedUpdateScript(_onFixedUpdateScript);
            }
            if (_onLaserHitScript != "")
            {
                SetOnLaserHitScript(_onLaserHitScript);
            }
            if (_onCollisionScript != "")
            {
                SetOnCollisionScript(_onCollisionScript);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _collision = collision;
            if (_onCollision != null)
            {
                try
                {
                    _onCollision.Invoke();
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
            }
            _collision = null;
        }
    }
}