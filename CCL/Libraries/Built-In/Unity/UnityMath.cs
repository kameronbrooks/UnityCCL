using CCL.Core;
using System;
using UnityEngine;

namespace CCL.Libraries
{
    public static class UnityMathLib
    {
        public class MathfType : TypeDef
        {
            public override string name
            {
                get
                {
                    return "Mathf";
                }
            }

            public override bool staticClass
            {
                get
                {
                    return true;
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(Mathf);
                }
            }
        }

        public class RandomType : TypeDef
        {
            public override string name
            {
                get
                {
                    return "Random";
                }
            }

            public override bool staticClass
            {
                get
                {
                    return true;
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(UnityEngine.Random);
                }
            }
        }

        public class TimeType : TypeDef
        {
            public override string name
            {
                get
                {
                    return "Time";
                }
            }

            public override bool staticClass
            {
                get
                {
                    return true;
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(Time);
                }
            }
        }
    }
}