using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using livecode.ComponentBase.AppPoint;
using livecode.ComponentBase.IdentificationPoint;
using livecode.wpf.Components.Logs;
using livecode.wpf.Listeners;
using livecode.wpf.Logs;

namespace livecode.wpf.Components.App
{
    internal class ProcessHookRequestManager : RequestManager
    {
        [ImportMany(typeof(ProcessHook))]
        internal List<ProcessHook> processHooks { get; set; }

        private Dictionary<int, KeyValuePair<Process, string>> _cmdCache = new Dictionary<int, KeyValuePair<Process, string>>();
        private Dictionary<RegexQuery, bool> _regxCache = new Dictionary<RegexQuery, bool>();
        
        public ProcessHookRequestManager()
        {
            GlobalTimer.ProcessesDiscovered += ProcessesDiscovered;
        }

        private void ProcessesDiscovered()
        {
            foreach (var hook in processHooks)
            {
                var processes = Process.GetProcessesByName(hook.ProcessName);

                foreach (var process in processes)
                {
                    string cmd = LookupCmdCache(process);
                    bool isMatch = LookupRegexCache(hook, cmd);

                    if (isMatch)
                    {
                        var type = hook.ProcessDiscovered(process.ProcessName, cmd);
                        DBInsertAgent.Instance.Visit(process, cmd, type);
                    }
                }
            }
        }

        private bool LookupRegexCache(ProcessHook hook, string cmd)
        {
            bool isMatch = false;
            var query = new RegexQuery(hook.CommandLineRegx, cmd);

            if (_regxCache.ContainsKey(query))
                isMatch = _regxCache[query];
            else
            {
                isMatch = Regex.IsMatch(cmd, hook.CommandLineRegx);

                _regxCache.Add(query, isMatch);
            }

            return isMatch;
        }

        private string LookupCmdCache(Process process)
        {
            string cmd = "";

            if (_cmdCache.ContainsKey(process.Id))
            {
                var p = _cmdCache[process.Id];
                if (p.Key.ProcessName == process.ProcessName)
                    cmd = p.Value;
                else
                    _cmdCache.Remove(process.Id);
            }
            else
            {
                cmd = getCommandLine(process);
                cmd = cmd.Replace("/", "\\").Replace(Properties.Settings.Default.ProjectDirectory.Replace("/", "\\"), "|PROJECT|");
                
                _cmdCache.Add(process.Id, new KeyValuePair<Process, string>(process, cmd));
            }

            return cmd;
        }

        private string getCommandLine(Process process)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            using (ManagementObjectCollection objects = searcher.Get())
            {
                return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }

        }

        private class RegexQuery
        {
            public string pattern { get; set; }
            public string input { get; set; }

            public RegexQuery(string _pattern, string _input)
            {
                pattern = _pattern;
                input = _input;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is RegexQuery)) return false;

                RegexQuery other = obj as RegexQuery;
                
                return string.Equals(pattern, other.pattern) && string.Equals(input, other.input);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((pattern != null ? pattern.GetHashCode() : 0) * 397) ^ (input != null ? input.GetHashCode() : 0);
                }
            }
        }
        
        #region Compose & Validate


        public override void Compose()
        {
            DirectoryCatalog catalog = new DirectoryCatalog(@"Plugins");
            CompositionContainer container = new CompositionContainer(catalog);

            container.ComposeParts(this);
        }

        public override void Validate()
        {
            for (var i = 0; i < processHooks.Count; i++)
            {
                var hook = processHooks[i];
                var component = Registration.Identify(hook.APIKey);

                if (!Registration.IsAllowed(hook.APIKey, Capablities.BackgroundProcesses))
                {
                    processHooks.RemoveAt(i);
                    i--;

                    continue;
                }

                Logger.Instance.Log($"Background Process hook initiated by: {component}");
            }
        }


        #endregion

    }
}
