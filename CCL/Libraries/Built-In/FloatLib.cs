using CCL.Core;
using System;
using UnityEngine;

namespace CCL.Libraries
{
    public static class FloatLib
    {
        [BinaryOperator(typeof(Single), Token.Type.Addition, typeof(Single))]
        public static object Add_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return temp0 + temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<float>)(() =>
                {
                    return temp0 + arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return arg0Func() + temp1;
                });
            }
            else
            {
                return (Func<float>)(() =>
                {
                    return arg0Func() + arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Assign, typeof(Single))]
        public static object Assign_float(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<float>(arg1Func, cdata);
            }
            else
            {
                float temp = (float)arg1;
                return ((Reference)arg0).CreateSetFunction<float>(temp, cdata);
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Assign, typeof(Int32))]
        public static object Assign_Int(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<float>((Func<float>)(() => { return (float)arg1Func(); }), cdata);
            }
            else
            {
                float temp = (float)(int)arg1;
                return ((Reference)arg0).CreateSetFunction<float>(temp, cdata);
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.AssignAdd, typeof(Single))]
        public static object AssignAdd_Float(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<float>(arg1Func, cdata, Token.Type.Addition);
            }
            else
            {
                float temp = (float)arg1;
                return ((Reference)arg0).CreateModifyFunction<float>(temp, cdata, Token.Type.Addition);
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.AssignDiv, typeof(Single))]
        public static object AssignDiv_Float(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<float>(arg1Func, cdata, Token.Type.Division);
            }
            else
            {
                float temp = (float)arg1;
                return ((Reference)arg0).CreateModifyFunction<float>(temp, cdata, Token.Type.Division);
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.AssignMult, typeof(Single))]
        public static object AssignMult_Float(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<float>(arg1Func, cdata, Token.Type.Multiplication);
            }
            else
            {
                float temp = (float)arg1;
                return ((Reference)arg0).CreateModifyFunction<float>(temp, cdata, Token.Type.Multiplication);
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.AssignSubtract, typeof(Single))]
        public static object AssignSubtract_Float(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<float>(arg1Func, cdata, Token.Type.Subtraction);
            }
            else
            {
                float temp = (float)arg1;
                return ((Reference)arg0).CreateModifyFunction<float>(temp, cdata, Token.Type.Subtraction);
            }
        }

        [UnaryOperator(typeof(Single), Token.Type.Decrement)]
        public static object Decrement_Float(object arg0, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            return ((Reference)arg0).CreateModifyFunction<float>(1.0f, cdata, Token.Type.Subtraction);
        }

        [BinaryOperator(typeof(Single), Token.Type.Division, typeof(Single))]
        public static object Divide_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return temp0 / temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<float>)(() =>
                {
                    return temp0 / arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return arg0Func() / temp1;
                });
            }
            else
            {
                return (Func<float>)(() =>
                {
                    return arg0Func() / arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Equals, typeof(Single))]
        public static object Equals_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 == temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 == arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() == temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() == arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.GreaterThan, typeof(Single))]
        public static object GreaterThan_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 > temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 > arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() > temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() > arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.GreaterThan, typeof(Int32))]
        public static object GreaterThan_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 > temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 > arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() > temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() > arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.GreaterThanOrEqualTo, typeof(Single))]
        public static object GreaterThanOrEqualTo_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 >= temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 >= arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() >= temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() >= arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.GreaterThanOrEqualTo, typeof(Int32))]
        public static object GreaterThanOrEqualTo_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 >= temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 >= arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() >= temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() >= arg1Func();
                });
            }
        }

        [UnaryOperator(typeof(Single), Token.Type.Increment)]
        public static object Increment_Float(object arg0, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            return ((Reference)arg0).CreateModifyFunction<float>(1.0f, cdata, Token.Type.Addition);
        }

        [BinaryOperator(typeof(Single[]), Token.Type.IsNotSubsetOf, typeof(Single[]))]
        public static object IsArrayNotSubsetOf_FloatArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<float[]> arg0Func = CompilerUtility.ForceGetFunction<float[]>(arg0, cdata);
            Func<float[]> arg1Func = CompilerUtility.ForceGetFunction<float[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float[] temp0 = (float[])arg0;
                float[] temp1 = (float[])arg1;
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)temp0, (Array)temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float[] temp0 = (float[])arg0;
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)temp0, (Array)arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float[] temp1 = (float[])arg1;
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)arg0Func(), (Array)temp1);
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)arg0Func(), (Array)arg1Func());
                });
            }
        }

        [BinaryOperator(typeof(Single[]), Token.Type.IsSubsetOf, typeof(Single[]))]
        public static object IsArraySubsetOf_FloatArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<float[]> arg0Func = CompilerUtility.ForceGetFunction<float[]>(arg0, cdata);
            Func<float[]> arg1Func = CompilerUtility.ForceGetFunction<float[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float[] temp0 = (float[])arg0;
                float[] temp1 = (float[])arg1;
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)temp0, (Array)temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float[] temp0 = (float[])arg0;
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)temp0, (Array)arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float[] temp1 = (float[])arg1;
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)arg0Func(), (Array)temp1);
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)arg0Func(), (Array)arg1Func());
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.IsNotSubsetOf, typeof(Single[]))]
        public static object IsNotSubsetOf_FloatArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float[]> arg1Func = CompilerUtility.ForceGetFunction<float[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float[] temp1 = (float[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf((Array)temp1, temp0) < 0;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), temp0) < 0;
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float[] temp1 = (float[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(temp1, arg0Func()) < 0;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), arg0Func()) < 0;
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.IsSubsetOf, typeof(Single[]))]
        public static object IsSubsetOf_FloatArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float[]> arg1Func = CompilerUtility.ForceGetFunction<float[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float[] temp1 = (float[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf((Array)temp1, temp0) > -1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), temp0) > -1;
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float[] temp1 = (float[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(temp1, arg0Func()) > -1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), arg0Func()) > -1;
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.LessThan, typeof(Single))]
        public static object LessThan_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 < temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 < arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() < temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() < arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.LessThan, typeof(Int32))]
        public static object LessThan_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 < temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 < arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() < temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() < arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.LessThanOrEqualTo, typeof(Single))]
        public static object LessThanOrEqualTo_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 <= temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 <= arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() <= temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() <= arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.LessThanOrEqualTo, typeof(Int32))]
        public static object LessThanOrEqualTo_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 <= temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 <= arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() <= temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() <= arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Modulo, typeof(Single))]
        public static object Modulo_Float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return temp0 % temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<float>)(() =>
                {
                    return temp0 % arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return arg0Func() % temp1;
                });
            }
            else
            {
                return (Func<float>)(() =>
                {
                    return arg0Func() % arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Multiplication, typeof(Single))]
        public static object Multiply_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return temp0 * temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<float>)(() =>
                {
                    return temp0 * arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return arg0Func() * temp1;
                });
            }
            else
            {
                return (Func<float>)(() =>
                {
                    return arg0Func() * arg1Func();
                });
            }
        }

        [UnaryOperator(typeof(Single), Token.Type.Subtraction)]
        public static object Negate(object arg0, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            if (arg0Func == null)
            {
                float temp = (float)arg0;
                return (Func<float>)(() =>
                {
                    return -temp;
                });
            }
            else
            {
                return (Func<float>)(() =>
                {
                    return -arg0Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.NotEquals, typeof(Single))]
        public static object NotEquals_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 != temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 != arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() != temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() != arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Power, typeof(Single))]
        public static object Power_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return (float)Mathf.Pow(temp0, temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<float>)(() =>
                {
                    return (float)Mathf.Pow(temp0, arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return (float)Mathf.Pow(arg0Func(), temp1);
                });
            }
            else
            {
                return (Func<float>)(() =>
                {
                    return (float)Mathf.Pow(arg0Func(), arg1Func());
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Power, typeof(Int32))]
        public static object Power_int(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)(int)arg1;
                return (Func<float>)(() =>
                {
                    return (float)Mathf.Pow(temp0, temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<float>)(() =>
                {
                    return (float)Mathf.Pow(temp0, arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)(int)arg1;
                return (Func<float>)(() =>
                {
                    return (float)Mathf.Pow(arg0Func(), temp1);
                });
            }
            else
            {
                return (Func<float>)(() =>
                {
                    return (float)Mathf.Pow(arg0Func(), arg1Func());
                });
            }
        }

        [DelegateSignature(typeof(Action<Single>))]
        public static object Signature_Action_Float(object func, object[] args)
        {
            ((Action<float>)func)(
                (args[0].GetType() == typeof(int)) ? (float)(int)args[0] : (float)args[0]
                );
            return null;
        }

        [DelegateSignature(typeof(Action<Single, Single>))]
        public static object Signature_Action_Float2(object func, object[] args)
        {
            ((Action<float, float>)func)(
                (args[0].GetType() == typeof(int)) ? (int)args[0] : (float)args[0],
                (args[1].GetType() == typeof(int)) ? (int)args[1] : (float)args[1]
                );
            return null;
        }

        [DelegateSignature(typeof(Action<Single, Single, Single>))]
        public static object Signature_Action_Float3(object func, object[] args)
        {
            ((Action<float, float, float>)func)(
                (args[0].GetType() == typeof(int)) ? (int)args[0] : (float)args[0],
                (args[1].GetType() == typeof(int)) ? (int)args[1] : (float)args[1],
                (args[2].GetType() == typeof(int)) ? (int)args[2] : (float)args[2]
                );
            return null;
        }

        [DelegateSignature(typeof(Func<Single>))]
        public static object Signature_Func_Float(object func, object[] args)
        {
            return ((Func<float>)func)();
        }

        [DelegateSignature(typeof(Func<Single[], Single>))]
        public static object Signature_Func_Float_FloatArr(object func, object[] args)
        {
            return ((Func<float[], float>)func)((float[])args[0]);
        }

        [DelegateSignature(typeof(Func<Single, Single>))]
        public static object Signature_Func_Float2(object func, object[] args)
        {
            return ((Func<float, float>)func)(
                (args[0].GetType() == typeof(int)) ? (int)args[0] : (float)args[0]
                );
        }

        [DelegateSignature(typeof(Func<Single, Single, Single>))]
        public static object Signature_Func_Float3(object func, object[] args)
        {
            return ((Func<float, float, float>)func)(
                (args[0].GetType() == typeof(int)) ? (int)args[0] : (float)args[0],
                (args[1].GetType() == typeof(int)) ? (int)args[1] : (float)args[1]
                );
        }

        [BinaryOperator(typeof(Single), Token.Type.Subtraction, typeof(Single))]
        public static object Subtract_float(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return temp0 - temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<float>)(() =>
                {
                    return temp0 - arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<float>)(() =>
                {
                    return arg0Func() - temp1;
                });
            }
            else
            {
                return (Func<float>)(() =>
                {
                    return arg0Func() - arg1Func();
                });
            }
        }

        public class Float : TypeDef
        {
            public override string[] alias
            {
                get
                {
                    return new string[] { "Single" };
                }
            }

            public override string name
            {
                get
                {
                    return "float";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(float);
                }
            }

            public override object CallFunction(object func)
            {
                return ((Func<float>)func)();
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(bool))
                    {
                        Func<bool> func = CompilerUtility.ForceGetFunction<bool>(arg, _internal);
                        return (Func<float>)(() => { return func() ? 1.0f : 0.0f; });
                    }
                    if (argType == typeof(sbyte))
                    {
                        Func<sbyte> func = CompilerUtility.ForceGetFunction<sbyte>(arg, _internal);
                        return (Func<float>)(() => { return (float)func(); });
                    }
                    else if (argType == typeof(int))
                    {
                        Func<int> func = CompilerUtility.ForceGetFunction<int>(arg, _internal);
                        return (Func<float>)(() => { return (float)func(); });
                    }
                    else if (argType == typeof(float))
                    {
                        Func<float> func = CompilerUtility.ForceGetFunction<float>(arg, _internal);
                        return func;
                    }
                    else if (argType == typeof(string))
                    {
                        Func<string> func = CompilerUtility.ForceGetFunction<string>(arg, _internal);
                        return (Func<float>)(() => { return Single.Parse(func()); });
                    }
                    else if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<float>)(() => { return (float)func(); });
                    }
                }
                else
                {
                    if (argType == typeof(bool))
                    {
                        bool temp = (bool)arg;
                        return (Func<float>)(() => { return temp ? 1.0f : 0.0f; });
                    }
                    if (argType == typeof(sbyte))
                    {
                        sbyte temp = (sbyte)arg;
                        return (Func<float>)(() => { return (float)temp; });
                    }
                    else if (argType == typeof(int))
                    {
                        int temp = (int)arg;
                        return (Func<float>)(() => { return (float)temp; });
                    }
                    else if (argType == typeof(float))
                    {
                        float temp = (float)arg;
                        return (Func<float>)(() => { return temp; });
                    }
                    else if (argType == typeof(string))
                    {
                        string temp = (string)arg;
                        return (Func<float>)(() => { return Single.Parse(temp); });
                    }
                    else if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<float>)(() => { return (float)temp; });
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
                        return (Func<float[]>)(() => { return (float[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<float[]>)(() => { return (float[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type float[] and " + argType.Name);
            }

            public override object CreateArrayLiteral(object[] elements, CompilerData cdata)
            {
                return CreateArrayLiteral<float>(elements, cdata);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return 0.0f; };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<float>))
                {
                    return () =>
                    {
                        return ((Func<float>)(arg))();
                    };
                }
                else if (arg.GetType() == typeof(Func<float[]>))
                {
                    return () =>
                    {
                        return ((Func<float[]>)(arg))();
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