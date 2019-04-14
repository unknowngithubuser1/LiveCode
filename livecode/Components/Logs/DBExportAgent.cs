using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Collections;
using KaVEData.Tools;
using livecode.wpf.Components.Logs.Models;
using livecode.wpf.Logs;
using livecode.wpf.MVVM.Models;
using livecode.wpf.Util;

namespace livecode.wpf.Components.Logs
{
    public class DBExportAgent
    {
        #region Singleton

        private static DBExportAgent _instance;
        private static object _lock = true;

        public static DBExportAgent Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new DBExportAgent());
                }
            }
        }

        public static void Initialize()
        {
            if (_instance == null) _instance = new DBExportAgent();
        }

        #endregion

        private DBExportAgent()
        {
            
        }

        private static string exportDir = Environment.CurrentDirectory + "\\Export\\";
        private KaVEEntities _baseContext = new KaVEEntities();

        public async void SaveToDisk()
        {
            if (!Directory.Exists(exportDir))
                Directory.CreateDirectory(exportDir);

            var query = _baseContext.Database.SqlQuery<string>("SELECT * from dbo.Events FOR XML AUTO");
            var codesq = _baseContext.Database.SqlQuery<string>("SELECT * from dbo.Events_CodeEvent FOR XML AUTO");

            List<string> result = await query.ToListAsync();
            List<string> codes = await codesq.ToListAsync();

            string file = exportDir + "\\Export-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string file2 = exportDir + "\\Codes-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            if (File.Exists(file))
                File.Delete(file);

            if (File.Exists(file2))
                File.Delete(file2);

            foreach (var chunk in result)
            {
                File.AppendAllText(file, chunk.Replace("><", ">\r\n<"), Encoding.Unicode);
            }

            foreach (var code in codes)
            {
                File.AppendAllText(file2, code.Replace("><", ">\r\n<"), Encoding.Unicode);
            }
        }

        public void DeleteEverything()
        {
            try
            {
                _baseContext.Database.ExecuteSqlCommand("DELETE FROM dbo.Events");
                _baseContext.Database.ExecuteSqlCommand("DELETE FROM dbo.Sessions");
            }
            catch (Exception e)
            {
                Logger.Instance.Log(e.ToString());
            }
        }
    }
}
