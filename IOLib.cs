using CCL.Core;
using System;
using System.IO;

public static class IOLib
{
    public class DirectoryType : TypeDef
    {
        public override string name
        {
            get
            {
                return "Directory";
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
                return typeof(Directory);
            }
        }
    }

    public class FileType : TypeDef
    {
        public override string name
        {
            get
            {
                return "File";
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
                return typeof(File);
            }
        }
    }
}