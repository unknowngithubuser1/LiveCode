using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.IdentificationPoint;
using livecode.wpf.Components.App;
using livecode.wpf.Components.CodeFile;
using livecode.wpf.Logs;

namespace livecode.wpf.Components
{
    internal class Registration
    {
        [ImportMany(typeof(ComponentIdentification))]
        private List<ComponentIdentification> registeredComponents { get; set; }

        public List<ComponentIdentification> Components => registeredComponents;

        private RequestManager[] managers =
        {
            new AppHookRequestManager(),
            new FileRequestManager(),
            new ProcessHookRequestManager()
        };
        
        #region Singleton

        private static Registration _instance;
        private static object _lock = true;

        public static Registration Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new Registration());
                }
            }
        }

        public static void Initialize()
        {
            if (_instance == null) _instance = new Registration();
        }

        #endregion

        private Registration()
        {
            composeComponents();
        }
        
        private void composeComponents()
        {
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(@"Plugins");
            CompositionContainer _directoryContainer = new CompositionContainer(directoryCatalog);

            _directoryContainer.ComposeParts(this);

            foreach (var component in registeredComponents)
            {
                Logger.Instance.Log($"Component Loaded: {component.Name}");
            }
            
            foreach (var manager in managers)
            {
                manager.Compose();
            }
        }
        
        public static string Identify(string key)
        {
            return Instance.registeredComponents.FirstOrDefault(c => c.Key.ToLower() == key.ToLower())?.Name;
        }

        public static bool IsAllowed(string key, Capablities permission)
        {
            return Instance.registeredComponents.FirstOrDefault(c => c.Key.ToLower() == key.ToLower())?.Permissions.HasFlag(permission) ?? false;
        }

        public static void Validate()
        {
            foreach (var manager in Instance.managers)
            {
                manager.Validate();
            }
        }
    }
}
