using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using livecode.ComponentBase.FilePoint;
using livecode.ComponentBase.IdentificationPoint;
using livecode.ComponentBase.Measures;
using livecode.ComponentBase.Util;
using livecode.wpf.Components.Logs;
using livecode.wpf.Components.Utilities;
using livecode.wpf.Listeners;
using livecode.wpf.Logs;
using livecode.wpf.MVVM.Models;
using livecode.wpf.MVVM.ViewModels.Locators;
using livecode.wpf.Util;
using CodeFileX = livecode.ComponentBase.FilePoint.CodeFile;

namespace livecode.wpf.Components.CodeFile
{
    internal class FileRequestManager : RequestManager
    {
        [ImportMany(typeof(FileHook))]
        internal List<FileHook> fileHooks { get; set; }

        private string _projectDir = Properties.Settings.Default.ProjectDirectory;

        public FileRequestManager()
        {
            ProjectFilesWatcher.OnFileCreate += OnOnFileCreate;
            ProjectFilesWatcher.OnFileRename += OnOnFileRename;
            ProjectFilesWatcher.OnFileChange += OnOnFileChange;
            ProjectFilesWatcher.OnFileDelete += OnOnFileDelete;

            GlobalTimer.NewFormsRequested += OnNewFormsRequested;
        }

        private  void notifyHook(Func<CodeFileX> how)
        {
            var result = how.Invoke();

            if (result != null)
                DBInsertAgent.Instance.Visit(result);

        }

        private void OnOnFileDelete(FileSystemEventArgs e)
        {
            //Logger.Instance.Log($"Project File change detected: {e.FullPath} : {e.ChangeType}");

            foreach (var hook in fileHooks)
                if (hook.CheckAddress(_projectDir, e.FullPath))
                    notifyHook(() => hook.FileDeleted(e.FullPath, e));

        }

        private void OnOnFileChange(FileSystemEventArgs e)
        {
            //Logger.Instance.Log($"Project File change detected: {e.FullPath} : {e.ChangeType}");

            foreach (var hook in fileHooks)
                if (hook.CheckAddress(_projectDir, e.FullPath))
                    notifyHook((() => hook.FileChanged(e.FullPath, e)));
        }

        private void OnOnFileRename(RenamedEventArgs e)
        {
           //Logger.Instance.Log($"Project File change detected: {e.FullPath} : {e.ChangeType}");

            foreach (var hook in fileHooks)
                if (hook.CheckAddress(_projectDir, e.FullPath))
                    notifyHook((() => hook.FileRenamed(e.FullPath, e)));

        }

        private void OnOnFileCreate(FileSystemEventArgs e)
        {
            //Logger.Instance.Log($"Project File change detected: {e.FullPath} : {e.ChangeType}");

            foreach (var hook in fileHooks)
                if (hook.CheckAddress(_projectDir, e.FullPath))
                    notifyHook((() => hook.FileCreated(e.FullPath, e)));
        }

        private void OnNewFormsRequested(DateTime start, DateTime end)
        {
            // Size Forms:
            foreach (var hook in fileHooks)
            {
                hook.MeasureCodeFiles<PSPSizeMeasure>();

                var forms = hook.Files.ToPSPForm();
                if (forms.Length > 0)
                    wpf.App.VMLocator<PSPFormsVMLocator>().PopulateSizeForms(forms);
            }

            // Time Forms:
            try
            {
                TimeFormModel form = DBQueryAgent.Instance.GetTimeLogs(start, end);
                wpf.App.VMLocator<PSPFormsVMLocator>().PopulateTimeForms(new []{form});
            }
            catch (Exception e)
            {
                Logger.Instance.Log(e.ToString());
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
            for (var i = 0; i < fileHooks.Count; i++)
            {
                var hook = fileHooks[i];
                var component = Registration.Identify(hook.APIKey);

                if (!Registration.IsAllowed(hook.APIKey, Capablities.DirectoryScan))
                {
                    fileHooks.RemoveAt(i);
                    i--;

                    continue;
                }

                Logger.Instance.Log($"File hook initiated by: {component}");
            }
        }


        #endregion

    }
}
