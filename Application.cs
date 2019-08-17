using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Application : MonoBehaviour
{
    [SerializeField]
    private Text _inputText;

    [SerializeField]
    private InputField _outputText;

    private CCL.Core.Assembly _assembly;

    private CCL.Core.Compiler _compiler;
    private CCL.Core.CompiledScript _script;

    [SerializeField]
    private Text _compileTimeText;

    [SerializeField]
    private Text _executeTimeText;

    private CCL.CodeHelper _codeHelper;

    private void ForceAndroidManifestDependancies()
    {
        bool ioTest = File.Exists("Test");
        ioTest = ioTest | Directory.Exists("Test");
    }

    public class Context
    {
        private InputField _textOut;
        private int _x = 0;

        public int x
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public int[] CreateIntArray(int len)
        {
            return new int[len];
        }

        public void Increment()
        {
            _x += 1;
        }

        public void Log(string text)
        {
            _textOut.text += "\n" + text;
        }

        public Context(InputField textOut)
        {
            _textOut = textOut;
        }

        public GameObject NewGameObject()
        {
            return new GameObject();
        }
    }

    // Use this for initialization
    private void Start()
    {
        ForceAndroidManifestDependancies();
        _assembly = CCL.Core.Assembly.main;
        MobileConsole.Log(" Initializing...");
        System.Type[] libs = CCL.Core.Assembly.main.GetLoadedLibraries();
        MobileConsole.Log("libs loaded: " + libs.Length);
        if (libs.Length < 1)
        {
            CCL.ScriptUtility.LoadStandardLibraries();

            libs = CCL.Core.Assembly.main.GetLoadedLibraries();
        }
        CCL.Core.Assembly.main.LoadLibrary(typeof(IOLib));
        MobileConsole.Log("libs loaded: " + libs.Length);
        for (int i = 0; i < libs.Length; i += 1)
        {
            MobileConsole.Log(libs[i].Name + " Loaded");
        }
        MobileConsole.Log("Done!");

        _codeHelper = new CCL.CodeHelper();
        _codeHelper.assembly = _assembly;
        _codeHelper.contextType = typeof(Context);
        _codeHelper.code = "";
    }

    public void OnHelp(string s)
    {
        string code = s;

        int i = code.Length - 1;
        while (i >= 0 && (char.IsLetterOrDigit(code[i]) || "[]()._".IndexOf(code[i]) > -1))
        {
            i--;
        }
        if (i < -1 || i > code.Length - 1) return;
        string var = code.Substring(i + 1, code.Length - (i + 1));
        //UnityEngine.Debug.Log("txt so far = " + var);
        _codeHelper.FindVariables(code);
        string[] predicitons = _codeHelper.Predict(var);
        foreach (string option in predicitons)
        {
            UnityEngine.Debug.Log("Option: " + option);
        }
    }

    public void OnCompile()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

#if UNITY_EDITOR
        _script = CCL.ScriptUtility.CompileScript(_inputText.text, typeof(Context));
        _script.SetContext(new Context(_outputText));
#else
        try
        {
            _script = CCL.ScriptUtility.CompileScript(_inputText.text, typeof(Context));
            _script.SetContext(new Context(_outputText));
        } catch (System.Exception e)
        {
            _outputText.text = "Compilation Error: " + e.Message;
        }
#endif

        watch.Stop();
        _compileTimeText.text = "Compile: " + watch.ElapsedMilliseconds + "ms";
    }

    public void Execute()
    {
        _outputText.text = "";
        Stopwatch watch = new Stopwatch();
        watch.Start();

#if UNITY_EDITOR
        object result = _script.Invoke();
        _outputText.text += (result != null) ? result.ToString() : "No Return Statement";
#else
        try
        {
            object result = _script.function();
            _outputText.text += (result != null) ? result.ToString() : "No Return Statement";
        } catch(System.Exception e)
        {
            _outputText.text = "Execution Failure: " + e.Message;
        }
#endif
        watch.Stop();
        _executeTimeText.text = "Execute: " + watch.ElapsedMilliseconds + "ms";
    }

    // Update is called once per frame
    private void Update()
    {
    }
}