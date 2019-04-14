using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Text.RegularExpressions;
using KaVEData.Tools;
using livecode.Common.Events;
using livecode.ComponentBase.AppPoint;
using livecode.ComponentBase.IdentificationPoint;
using livecode.wpf.Components.Logs;
using livecode.wpf.Listeners;
using livecode.wpf.Logs;

namespace livecode.wpf.Components.App
{
    internal class AppHookRequestManager : RequestManager
    {
        [ImportMany(typeof(AppHook))]
        internal List<AppHook> appHooks { get; set; }
        
        public AppHookRequestManager()
        {
            GlobalTimer.ForegroundAppChanged += ForegroundChanged;
            GlobalTimer.ForegroundAppsDiscovered += ForegroundsDiscovered;
        }

        private void ForegroundsDiscovered(ForegroundApp[] apps)
        {
            foreach (var app in apps)
            {
                foreach (var hook in appHooks)
                {
                    // Notify Activation
                    if (Regex.IsMatch(app.WindowTitle, hook.WindowTitleRegx) &&
                        Regex.IsMatch(app.ProcessName, hook.ProcessNameRegx))
                    {
                        AppType type = hook.ResolveType(app.WindowTitle, app.ProcessName);
                        DBInsertAgent.Instance.Visit(app, type.ToString());
                    }
                }
            }
        }

        public void ForegroundChanged(ForegroundApp old, ForegroundApp current)
        {
            Logger.Instance.Log(old.Details);
            Logger.Instance.Log(current.Details);

            foreach (var hook in appHooks)
            {
                // Notify Deactivation
                if (old.WindowTitle != null &&
                    Regex.IsMatch(old.WindowTitle, hook.WindowTitleRegx) &&
                    Regex.IsMatch(old.ProcessName, hook.ProcessNameRegx))
                {
                    hook.WindowDeactivated(old.WindowTitle, old.ProcessName);
                }

                // Notify Activation
                if (Regex.IsMatch(current.WindowTitle, hook.WindowTitleRegx) &&
                    Regex.IsMatch(current.ProcessName, hook.ProcessNameRegx))
                {
                    var type = hook.WindowActivated(current.WindowTitle, current.ProcessName);
                    DBInsertAgent.Instance.Visit(current, type.ToString(), true);
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
            for (var i = 0; i < appHooks.Count; i++)
            {
                var hook = appHooks[i];
                var component = Registration.Identify(hook.APIKey);

                if (!Registration.IsAllowed(hook.APIKey, Capablities.ForegroundApp))
                {
                    appHooks.RemoveAt(i);
                    i--;

                    continue;
                }

                Logger.Instance.Log($"Foreground App hook initiated by: {component}");
            }
        }


        #endregion

    }
}
