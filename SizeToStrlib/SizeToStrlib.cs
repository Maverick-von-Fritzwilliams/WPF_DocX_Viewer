using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeToStrlib
{
    public class SizeToStrlib
    { 
        public static string Size_ToStr(long size) //Converting long to string and bytes to Kb/Mb
        {
            float size_float;
            if (size >= 1000)
            {
                size_float = (float)Math.Round((float)size / 1000, 1);
                if (size_float >= 1000)
                {
                    size_float = (float)Math.Round(size_float / 1000, 1);
                    return size_float.ToString() + "MB";
                }
                else
                    return size_float.ToString() + "KB";
            }
            else
                return size.ToString() + "B";
        }
    }
}
