using UnityEngine;
using UnityEngine.UI;

namespace PhysicsDemo
{
    public class SpaceCCLDemo : MonoBehaviour
    {
        [SerializeField]
        private BoxModal _boxModal;

        private CCL.Core.CompiledScript _compiledScript;
        private SpaceSceneContext _context;

        [SerializeField]
        private Modal _modal;

        private string _source;

        [SerializeField]
        private InputField _sourceInput;

        public void CompileScript()
        {
            // Compile the ccl source with the utility function
            // pass in the context type
            try
            {
                _source = _sourceInput.text;
                _compiledScript = CCL.ScriptUtility.CompileScript(_source, typeof(SpaceSceneContext));
                _compiledScript.SetContext(_context);
            }
            catch (System.Exception e)
            {
                _modal.ShowMessage(e.ToString());
                _compiledScript = null;
            }
        }

        public void RunCompiledScript()
        {
            // if compiled script is not null, run it
            if (_compiledScript != null)
            {
                try
                {
                    _compiledScript.Invoke();
                }
                catch (System.Exception e)
                {
                    _modal.ShowMessage(e.ToString());
                }
            }
        }

        public void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            if (!Input.GetMouseButtonDown(1)) return;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

            if (hit.collider)
            {
                Box box = hit.collider.gameObject.GetComponent<Box>();
                if (box != null)
                {
                    _boxModal.Open(box);
                }
            }
            else if (false)
            {
            }
            else
            {
                _boxModal.Close();
            }
        }

        // Use this for initialization
        private void Start()
        {
            Physics2D.gravity = Vector2.zero;
            // Unload assembly if alredy loaded
            CCL.Core.Assembly.main.Unload();
            // Load the standard libraries into the CCL Assembly
            CCL.ScriptUtility.LoadStandardLibraries();

            _context = FindObjectOfType<SpaceSceneContext>();

            _sourceInput.text = @"
object box = CreateBox();
Vector2 vel = [2.0,2.0];

box.body.velocity = vel;

box.renderer.color = (Color)([1.0, 1.0, 0.0, 1.0]);";
        }
    }
}