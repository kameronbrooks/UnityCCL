using CCL.Core;
using System;
using UnityEngine;

namespace CCL.Libraries
{
    public static class IntegerLib
    {
        [BinaryOperator(typeof(Int32), Token.Type.Addition, typeof(Int32))]
        public static object Add_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 + temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 + arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() + temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() + arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.Assign, typeof(Int32))]
        public static object Assign_Int(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<int>(arg1Func, cdata);
            }
            else
            {
                int temp = (int)arg1;
                return ((Reference)arg0).CreateSetFunction<int>(temp, cdata);
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.AssignAdd, typeof(Int32))]
        public static object AssignAdd_Int(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<int>(arg1Func, cdata, Token.Type.Addition);
            }
            else
            {
                int temp = (int)arg1;
                return ((Reference)arg0).CreateModifyFunction<int>(temp, cdata, Token.Type.Addition);
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.AssignDiv, typeof(Int32))]
        public static object AssignDiv_Int(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<int>(arg1Func, cdata, Token.Type.Division);
            }
            else
            {
                int temp = (int)arg1;
                return ((Reference)arg0).CreateModifyFunction<int>(temp, cdata, Token.Type.Division);
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.AssignMult, typeof(Int32))]
        public static object AssignMult_Int(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<int>(arg1Func, cdata, Token.Type.Multiplication);
            }
            else
            {
                int temp = (int)arg1;
                return ((Reference)arg0).CreateModifyFunction<int>(temp, cdata, Token.Type.Multiplication);
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.AssignSubtract, typeof(Int32))]
        public static object AssignSubtract_Int(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<int>(arg1Func, cdata, Token.Type.Subtraction);
            }
            else
            {
                int temp = (int)arg1;
                return ((Reference)arg0).CreateModifyFunction<int>(temp, cdata, Token.Type.Subtraction);
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.BitwiseAnd, typeof(Int32))]
        public static object BitwiseAnd_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 & temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 & arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() & temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() & arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.BitwiseLeftShift, typeof(Int32))]
        public static object BitwiseLeftShift_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 << temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 << arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() << temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() << arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.BitwiseOr, typeof(Int32))]
        public static object BitwiseOr_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 | temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 | arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() | temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() | arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.BitwiseRightShift, typeof(Int32))]
        public static object BitwiseRightShift_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 >> temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 >> arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() >> temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() >> arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.BitwiseXOr, typeof(Int32))]
        public static object BitwiseXOR_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 ^ temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 ^ arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() ^ temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() ^ arg1Func();
                });
            }
        }

        [UnaryOperator(typeof(Int32), Token.Type.Decrement)]
        public static object Decrement_Int(object arg0, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            return ((Reference)arg0).CreateModifyFunction<int>(1, cdata, Token.Type.Subtraction);
        }

        [BinaryOperator(typeof(Int32), Token.Type.Division, typeof(Int32))]
        public static object Divide_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 / temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 / arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() / temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() / arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.Equals, typeof(Int32))]
        public static object Equals_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 == temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 == arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
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

        [BinaryOperator(typeof(Int32), Token.Type.GreaterThan, typeof(Single))]
        public static object GreaterThan_Float(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 > temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
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

        [BinaryOperator(typeof(Int32), Token.Type.GreaterThan, typeof(Int32))]
        public static object GreaterThan_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 > temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
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

        [BinaryOperator(typeof(Int32), Token.Type.GreaterThanOrEqualTo, typeof(Single))]
        public static object GreaterThanOrEqualTo_Float(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 >= temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
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

        [BinaryOperator(typeof(Int32), Token.Type.GreaterThanOrEqualTo, typeof(Int32))]
        public static object GreaterThanOrEqualTo_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 >= temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
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

        [UnaryOperator(typeof(Int32), Token.Type.Increment)]
        public static object Increment_Int(object arg0, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            return ((Reference)arg0).CreateModifyFunction<int>(1, cdata, Token.Type.Addition);
        }

        [BinaryOperator(typeof(Int32[]), Token.Type.IsNotSubsetOf, typeof(Int32[]))]
        public static object IsArrayNotSubsetOf_IntArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<int[]> arg0Func = CompilerUtility.ForceGetFunction<int[]>(arg0, cdata);
            Func<int[]> arg1Func = CompilerUtility.ForceGetFunction<int[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int[] temp0 = (int[])arg0;
                int[] temp1 = (int[])arg1;
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)temp0, (Array)temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int[] temp0 = (int[])arg0;
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)temp0, (Array)arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int[] temp1 = (int[])arg1;
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

        [BinaryOperator(typeof(Int32[]), Token.Type.IsSubsetOf, typeof(Int32[]))]
        public static object IsArraySubsetOf_IntArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<int[]> arg0Func = CompilerUtility.ForceGetFunction<int[]>(arg0, cdata);
            Func<int[]> arg1Func = CompilerUtility.ForceGetFunction<int[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int[] temp0 = (int[])arg0;
                int[] temp1 = (int[])arg1;
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)temp0, (Array)temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int[] temp0 = (int[])arg0;
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)temp0, (Array)arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int[] temp1 = (int[])arg1;
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

        [BinaryOperator(typeof(Int32), Token.Type.IsNotSubsetOf, typeof(Int32[]))]
        public static object IsNotSubsetOf_IntArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int[]> arg1Func = CompilerUtility.ForceGetFunction<int[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int[] temp1 = (int[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf((Array)temp1, temp0) < 0;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), temp0) < 0;
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int[] temp1 = (int[])arg1;
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

        [BinaryOperator(typeof(Int32), Token.Type.IsSubsetOf, typeof(Int32[]))]
        public static object IsSubsetOf_IntArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int[]> arg1Func = CompilerUtility.ForceGetFunction<int[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int[] temp1 = (int[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf((Array)temp1, temp0) > -1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), temp0) > -1;
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int[] temp1 = (int[])arg1;
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

        [BinaryOperator(typeof(Int32), Token.Type.LessThan, typeof(Single))]
        public static object LessThan_Float(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 < temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
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

        [BinaryOperator(typeof(Int32), Token.Type.LessThan, typeof(Int32))]
        public static object LessThan_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 < temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
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

        [BinaryOperator(typeof(Int32), Token.Type.LessThanOrEqualTo, typeof(Single))]
        public static object LessThanOrEqualTo_Float(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                float temp1 = (float)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 <= temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
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

        [BinaryOperator(typeof(Int32), Token.Type.LessThanOrEqualTo, typeof(Int32))]
        public static object LessThanOrEqualTo_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 <= temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
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

        [BinaryOperator(typeof(Int32), Token.Type.Modulo, typeof(Int32))]
        public static object Modulo_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 % temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 % arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() % temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() % arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.Multiplication, typeof(Int32))]
        public static object Multiply_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 * temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 * arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() * temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() * arg1Func();
                });
            }
        }

        [UnaryOperator(typeof(Int32), Token.Type.Subtraction)]
        public static object Negate(object arg0, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            if (arg0Func == null)
            {
                int temp = (int)arg0;
                return (Func<int>)(() =>
                {
                    return -temp;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return -arg0Func();
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.NotEquals, typeof(Int32))]
        public static object NotEquals_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 != temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 != arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
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

        [BinaryOperator(typeof(Int32), Token.Type.Power, typeof(Int32))]
        public static object Power_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return (int)Mathf.Pow(temp0, temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return (int)Mathf.Pow(temp0, arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return (int)Mathf.Pow(arg0Func(), temp1);
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return (int)Mathf.Pow(arg0Func(), arg1Func());
                });
            }
        }

        [DelegateSignature(typeof(Action<Int32>))]
        public static object Signature_Action_Int(object func, object[] args)
        {
            ((Action<int>)func)((int)args[0]);
            return null;
        }

        [DelegateSignature(typeof(Action<Int32, Int32>))]
        public static object Signature_Action_Int2(object func, object[] args)
        {
            ((Action<int, int>)func)((int)args[0], (int)args[1]);
            return null;
        }

        [DelegateSignature(typeof(Action<Int32, Int32, Int32>))]
        public static object Signature_Action_Int3(object func, object[] args)
        {
            ((Action<int, int, int>)func)((int)args[0], (int)args[1], (int)args[2]);
            return null;
        }

        [DelegateSignature(typeof(Func<Int32>))]
        public static object Signature_Func_Int(object func, object[] args)
        {
            return ((Func<int>)func)();
        }

        [DelegateSignature(typeof(Func<Int32[], Int32>))]
        public static object Signature_Func_Int_IntArr(object func, object[] args)
        {
            return ((Func<int[], int>)func)((int[])args[0]);
        }

        [DelegateSignature(typeof(Func<Int32, Int32>))]
        public static object Signature_Func_Int2(object func, object[] args)
        {
            return ((Func<int, int>)func)((int)args[0]);
        }

        [DelegateSignature(typeof(Func<Int32, Int32, Int32>))]
        public static object Signature_Func_Int3(object func, object[] args)
        {
            return ((Func<int, int, int>)func)((int)args[0], (int)args[1]);
        }

        [BinaryOperator(typeof(Int32), Token.Type.Interp, typeof(Int32))]
        public static object Spread_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int[]>)(() =>
                {
                    return ArrayUtility.CreateArrayBetween(temp0, temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int[]>)(() =>
                {
                    return ArrayUtility.CreateArrayBetween(temp0, arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int[]>)(() =>
                {
                    return ArrayUtility.CreateArrayBetween(arg0Func(), temp1);
                });
            }
            else
            {
                return (Func<int[]>)(() =>
                {
                    return ArrayUtility.CreateArrayBetween(arg0Func(), arg1Func());
                });
            }
        }

        [BinaryOperator(typeof(Int32), Token.Type.Subtraction, typeof(Int32))]
        public static object Subtract_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<int> arg0Func = CompilerUtility.ForceGetFunction<int>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                int temp0 = (int)arg0;
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return temp0 - temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                int temp0 = (int)arg0;
                return (Func<int>)(() =>
                {
                    return temp0 - arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<int>)(() =>
                {
                    return arg0Func() - temp1;
                });
            }
            else
            {
                return (Func<int>)(() =>
                {
                    return arg0Func() - arg1Func();
                });
            }
        }

        public class Integer : TypeDef
        {
            public override string[] alias
            {
                get
                {
                    return new string[] { "Int32" };
                }
            }

            public override string name
            {
                get
                {
                    return "int";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(int);
                }
            }

            public override object CallFunction(object func)
            {
                return ((Func<int>)func)();
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(bool))
                    {
                        Func<bool> func = CompilerUtility.ForceGetFunction<bool>(arg, _internal);
                        return (Func<int>)(() => { return func() ? 1 : 0; });
                    }
                    if (argType == typeof(sbyte))
                    {
                        Func<sbyte> func = CompilerUtility.ForceGetFunction<sbyte>(arg, _internal);
                        return (Func<int>)(() => { return (int)func(); });
                    }
                    else if (argType == typeof(int))
                    {
                        Func<int> func = CompilerUtility.ForceGetFunction<int>(arg, _internal);
                        return func;
                    }
                    else if (argType == typeof(float))
                    {
                        Func<float> func = CompilerUtility.ForceGetFunction<float>(arg, _internal);
                        return (Func<int>)(() => { return (int)func(); });
                    }
                    else if (argType == typeof(string))
                    {
                        Func<string> func = CompilerUtility.ForceGetFunction<string>(arg, _internal);
                        return (Func<int>)(() => { return Int32.Parse(func()); });
                    }
                    else if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction<object>(arg, _internal);
                        return (Func<int>)(() => { return (int)func(); });
                    }
                }
                else
                {
                    if (argType == typeof(bool))
                    {
                        bool temp = (bool)arg;
                        return (Func<int>)(() => { return temp ? 1 : 0; });
                    }
                    if (argType == typeof(sbyte))
                    {
                        sbyte temp = (sbyte)arg;
                        return (Func<int>)(() => { return (int)temp; });
                    }
                    else if (argType == typeof(int))
                    {
                        int temp = (int)arg;
                        return (Func<int>)(() => { return temp; });
                    }
                    else if (argType == typeof(float))
                    {
                        float temp = (float)arg;
                        return (Func<int>)(() => { return (int)temp; });
                    }
                    else if (argType == typeof(string))
                    {
                        string temp = (string)arg;
                        return (Func<int>)(() => { return Int32.Parse(temp); });
                    }
                    else if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<int>)(() => { return (int)temp; });
                    }
                }

                throw _internal.CreateException("No supported cast for type int and " + argType.Name);
            }

            public override object CastArray(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<int[]>)(() => { return (int[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<int[]>)(() => { return (int[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type int[] and " + argType.Name);
            }

            public override object CreateArrayLiteral(object[] elements, CompilerData cdata)
            {
                return CreateArrayLiteral<int>(elements, cdata);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return 0; };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<int>))
                {
                    return () =>
                    {
                        return ((Func<int>)(arg))();
                    };
                }
                else if (arg.GetType() == typeof(Func<int[]>))
                {
                    return () =>
                    {
                        return ((Func<int[]>)(arg))();
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