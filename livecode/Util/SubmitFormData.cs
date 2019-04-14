using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.wpf.MVVM.ViewModels;

namespace livecode.wpf.Util
{
    public class SubmitFormData
    {
        private static string submitDir = Environment.CurrentDirectory + "\\Submissions\\";

        public static void SaveTimeForm(string data)
        {
            if (!Directory.Exists(submitDir))
                Directory.CreateDirectory(submitDir);

            File.WriteAllText(submitDir + "\\TimeForm-" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".xml", data, Encoding.Unicode);
        }

        public static void SaveSizeForm(string data)
        {
            if (!Directory.Exists(submitDir))
                Directory.CreateDirectory(submitDir);

            File.WriteAllText(submitDir + "\\SizeForm-" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".xml", data, Encoding.Unicode);
        }
    }
}
