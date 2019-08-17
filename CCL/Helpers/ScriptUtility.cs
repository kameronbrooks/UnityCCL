#define UNITY

using CCL.Core;
using System;

namespace CCL
{
    public class ScriptUtility
    {
        public static void LoadStandardLibraries()
        {
            Assembly.main.LoadLibrary(typeof(Libraries.IntegerLib));
            Assembly.main.LoadLibrary(typeof(Libraries.BoolLib));
            Assembly.main.LoadLibrary(typeof(Libraries.FloatLib));
            Assembly.main.LoadLibrary(typeof(Libraries.StringLib));
            Assembly.main.LoadLibrary(typeof(Libraries.ObjectLib));
#if UNITY
            Assembly.main.LoadLibrary(typeof(Libraries.Vector2Lib));
            Assembly.main.LoadLibrary(typeof(Libraries.Vector3Lib));
            Assembly.main.LoadLibrary(typeof(Libraries.Vector4Lib));
            Assembly.main.LoadLibrary(typeof(Libraries.ColorLib));
            Assembly.main.LoadLibrary(typeof(Libraries.QuaternionLib));
            Assembly.main.LoadLibrary(typeof(Libraries.UnityRefTypesLib));
            Assembly.main.LoadLibrary(typeof(Libraries.UnityMathLib));
#endif
        }

        public static CompiledScript CompileScript(string script, Type contextType)
        {
            Lexer lexer = new Lexer(Assembly.main);
            Token[] tokens = lexer.Tokenize(script);
            Compiler compiler = new Compiler(tokens, Assembly.main, contextType);

            return compiler.Compile();
        }
    }
}