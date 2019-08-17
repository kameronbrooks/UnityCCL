using CCL.Core;
using System;
using UnityEngine;

namespace CCL.Libraries
{
    public static class UnityRefTypesLib
    {
        public class GameObjectType : TypeDef
        {
            public override bool allowStaticMembers
            {
                get
                {
                    return false;
                }
            }

            public override string name
            {
                get
                {
                    return "GameObject";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(GameObject);
                }
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<GameObject>)(() => { return (GameObject)func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<GameObject>)(() => { return (GameObject)temp; });
                    }
                }

                throw _internal.CreateException("No supported cast for type float and " + argType.Name);
            }

            public override object CastArray(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<GameObject[]>)(() => { return (GameObject[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<GameObject[]>)(() => { return (GameObject[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type Vector3[] and " + argType.Name);
            }

            public override Func<object> DefaultConstructor()
            {
                return (() => { return null; });
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<GameObject>))
                {
                    return () =>
                    {
                        return ((Func<GameObject>)(arg))();
                    };
                }
                else if (arg.GetType() == typeof(Func<GameObject[]>))
                {
                    return () =>
                    {
                        return ((Func<GameObject[]>)(arg))();
                    };
                }
                return () =>
                {
                    return arg;
                };
            }
        }

        public class TransformType : TypeDef
        {
            public override string name
            {
                get
                {
                    return "Transform";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(Transform);
                }
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<Transform>)(() => { return (Transform)func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<Transform>)(() => { return (Transform)temp; });
                    }
                }

                throw _internal.CreateException("No supported cast for type float and " + argType.Name);
            }

            public override object CastArray(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<Transform[]>)(() => { return (Transform[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<Transform[]>)(() => { return (Transform[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type Vector3[] and " + argType.Name);
            }

            public override Func<object> DefaultConstructor()
            {
                return (Func<object>)(() => { return null; });
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<Transform>))
                {
                    return () =>
                    {
                        return ((Func<Transform>)(arg))();
                    };
                }
                else if (arg.GetType() == typeof(Func<Transform[]>))
                {
                    return () =>
                    {
                        return ((Func<Transform[]>)(arg))();
                    };
                }
                return () =>
                {
                    return arg;
                };
            }
        }
    }
}