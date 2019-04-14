using System;

namespace livecode.ComponentBase.Util
{
    public static class StringHelper
    {
        public static string RemoveTempPostfix(this string filePath)
        {
            return filePath.Substring(Math.Max(0,filePath.LastIndexOf("~" + 1)));
        }
    }
}
