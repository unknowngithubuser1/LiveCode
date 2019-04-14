using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.wpf.Logs;
using livecode.wpf.MVVM.Models;
using livecode.wpf.MVVM.ViewModels;

namespace livecode.wpf.Util
{
    public class LoadFormData
    {
        private static string submitDir = Environment.CurrentDirectory + "\\Submissions\\";

        public static TimeFormModel[] LoadTimeForms()
        {
            return LoadForm("TimeForm").Select(DeserializeTimeForm).ToArray();
        }

        public static SizeFormModel[] LoadSizeForms()
        {
            return LoadForm("SizeForm").Select(DeserializeSizeForm).ToArray();
        }

        private static string[] LoadForm(string type)
        {
            if (!Directory.Exists(submitDir))
                return null;

            var files = Directory.EnumerateFiles(submitDir, $"{type}*.xml").ToArray();
            if (files.Length == 0)
                return null;

            List<string> forms = new List<string>();
            foreach (var file in files)
            {
                try
                {
                    forms.Add(File.ReadAllText(file, Encoding.Unicode));
                }
                catch (Exception e)
                {
                    Logger.Instance.Log(e.ToString());
                }
            }

            return forms.ToArray();
        }

        public static TimeFormModel DeserializeTimeForm(string data)
        {
            var array = TextSerializer.XmlDeserializeFromString<TimeFormModel[]>(data);
            return array[0];
        }

        public static SizeFormModel DeserializeSizeForm(string data)
        {
            var array = TextSerializer.XmlDeserializeFromString<SizeFormModel[]>(data);
            return array[0];
        }
    }
}
