using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCL
{
    public class ArrayUtility
    {
        public static object AddToElementInArray(object arr, int index, object element)
        {
            Type arrType = arr.GetType();
            if (arrType == typeof(int[]))
            {
                return ((int[])arr)[index] += (int)element;
            }
            else if (arrType == typeof(float[]))
            {
                return ((float[])arr)[index] += (float)element;
            }
            else if (arrType == typeof(string[]))
            {
                return ((string[])arr)[index] += (string)element;
            }
            if ((arr as IList<int>) != null)
            {
                return ((IList<int>)arr)[index] += (int)element;
            }
            else if ((arr as IList<float>) != null)
            {
                return ((IList<float>)arr)[index] += (float)element;
            }
            else if ((arr as IList<string>) != null)
            {
                return ((IList<string>)arr)[index] += (string)element;
            }
            else
            {
                throw new Exception("cannot increment or decrement an element of type " + arrType.Name);
            }
        }

        static public T[] ConvertArray<T, U>(U[] arr) where T : U
        {
            T[] output = new T[arr.Length];
            for (int i = 0; i < arr.Length; i += 1)
            {
                output[i] = (T)arr[i];
            }
            return output;
        }

        public static int[] CreateArrayBetween(int a, int b)
        {
            int dif = b - a;
            int dist = Mathf.Abs(dif);

            if (dif == 0)
            {
                return new int[1] {
                    a
                };
            }

            int step = dif / dist;

            int[] arr = new int[dist + 1];
            for (int i = 0; i <= dist; i += 1)
            {
                arr[i] = a + step * i;
            }
            return arr;
        }

        public static object DecrementElementInArray(object arr, int index)
        {
            Type arrType = arr.GetType();
            if (arrType == typeof(int[]))
            {
                return ((int[])arr)[index]--;
            }
            else if (arrType == typeof(float[]))
            {
                return ((float[])arr)[index]--;
            }
            if ((arr as IList<int>) != null)
            {
                return ((IList<int>)arr)[index]--;
            }
            else if ((arr as IList<float>) != null)
            {
                return ((IList<float>)arr)[index]--;
            }
            else
            {
                throw new Exception("cannot increment or decrement an element of type " + arrType.Name);
            }
        }

        public static object DivideElementInArrayBy(object arr, int index, object element)
        {
            Type arrType = arr.GetType();
            if (arrType == typeof(int[]))
            {
                return ((int[])arr)[index] /= (int)element;
            }
            else if (arrType == typeof(float[]))
            {
                return ((float[])arr)[index] /= (float)element;
            }
            if ((arr as IList<int>) != null)
            {
                return ((IList<int>)arr)[index] /= (int)element;
            }
            else if ((arr as IList<float>) != null)
            {
                return ((IList<float>)arr)[index] /= (float)element;
            }
            else
            {
                throw new Exception("cannot increment or decrement an element of type " + arrType.Name);
            }
        }

        public static object GetElementFromArray(object ob, int index)
        {
            Array arr = ob as Array;
            if (arr != null) return arr.GetValue(index);

            IList list = ob as IList;
            if (list != null) return list[index];
            // 50 times slower than a normal array access
            return null;
        }

        public static Type GetElementType(Type listType)
        {
            if (listType == null) return null;
            Type t = listType.GetElementType();
            if (t != null) return t;

            if (listType.IsGenericType)
            {
                t = listType.GetGenericArguments()[0];
                if (t != null) return t;
            }
            return null;
        }

        public static bool HasUnion(Array ar0, Array ar1)
        {
            if (ar0.Length > ar1.Length)
            {
                Array temp = ar0;
                ar0 = ar1;
                ar1 = temp;
            }
            for (int i = 0; i < ar0.Length; i += 1)
            {
                if (Array.IndexOf(ar1, ar0.GetValue(i)) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static object IncrementElementInArray(object arr, int index)
        {
            Type arrType = arr.GetType();
            if (arrType == typeof(int[]))
            {
                return ((int[])arr)[index]++;
            }
            else if (arrType == typeof(float[]))
            {
                return ((float[])arr)[index]++;
            }
            if ((arr as IList<int>) != null)
            {
                return ((IList<int>)arr)[index]++;
            }
            else if ((arr as IList<float>) != null)
            {
                return ((IList<float>)arr)[index]++;
            }
            else
            {
                throw new Exception("cannot increment or decrement an element of type " + arrType.Name);
            }
        }

        public static bool IsSubsetOf(Array ar0, Array ar1)
        {
            for (int i = 0; i < ar0.Length; i += 1)
            {
                if (Array.IndexOf(ar1, ar0.GetValue(i)) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static object MultiplyElementInArrayBy(object arr, int index, object element)
        {
            Type arrType = arr.GetType();
            if (arrType == typeof(int[]))
            {
                return ((int[])arr)[index] *= (int)element;
            }
            else if (arrType == typeof(float[]))
            {
                return ((float[])arr)[index] *= (float)element;
            }
            if ((arr as IList<int>) != null)
            {
                return ((IList<int>)arr)[index] *= (int)element;
            }
            else if ((arr as IList<float>) != null)
            {
                return ((IList<float>)arr)[index] *= (float)element;
            }
            else
            {
                throw new Exception("cannot increment or decrement an element of type " + arrType.Name);
            }
        }

        public static void SetElementInArray(object ob, int index, object element)
        {
            // 50 times slower than a normal array access
            // 12 times slower than a normal array access
            Array arr = ob as Array;
            if (arr != null)
            {
                arr.SetValue(element, index);
                return;
            }

            IList list = ob as IList;
            if (list != null) list[index] = element;
        }

        public static object SubtractFromElementInArray(object arr, int index, object element)
        {
            Type arrType = arr.GetType();
            if (arrType == typeof(int[]))
            {
                return ((int[])arr)[index] -= (int)element;
            }
            else if (arrType == typeof(float[]))
            {
                return ((float[])arr)[index] -= (float)element;
            }
            if ((arr as IList<int>) != null)
            {
                return ((IList<int>)arr)[index] -= (int)element;
            }
            else if ((arr as IList<float>) != null)
            {
                return ((IList<float>)arr)[index] -= (float)element;
            }
            else
            {
                throw new Exception("cannot increment or decrement an element of type " + arrType.Name);
            }
        }
    }
}