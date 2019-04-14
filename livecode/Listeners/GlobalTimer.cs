using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using livecode.Common.Events;
using livecode.wpf.Logs;
using ManagedWinapi;
using ManagedWinapi.Hooks;
using ManagedWinapi.Mods;
using ManagedWinapi.Windows;

namespace livecode.wpf.Listeners
{
    public class GlobalTimer
    {
        private static int _msTrigger = 7000;
        private bool _isEnabled;
        private DateTime _lastPrompt;
        private bool _isBusy;

        private Timer t = new Timer(_msTrigger);
        //LowLevelKeyboardListener _keyboard = new LowLevelKeyboardListener();
        //ClipboardNotifier _clipboard = new ClipboardNotifier();

        public DateTime LastPrompt => _lastPrompt.AddMilliseconds(0.1);

        public GlobalTimer()
        {
            //_keyboard.OnKeyPressed += KeyboardOnOnKeyPressed;
            //_keyboard.HookKeyboard();

            //_clipboard.ClipboardChanged += OnClipboardChanged;

            t.Elapsed += T_Elapsed;
            _lastPrompt = DateTime.Now;
        }

        public void Start()
        {
            _isEnabled = true;
            t.Start();
        }

        public void Stop()
        {
            _isEnabled = false;
            t.Stop();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                ActivityCheck();
                ForegroundAppUpdate();
                ProcessesUpdate();
                
                if (!_isBusy && DateTime.Now.Subtract(_lastPrompt).TotalMinutes > Properties.Settings.Default.FormInterval)
                    PrepareForms();
            }
            catch (Exception exception)
            {
                Logger.Instance.Log(exception.ToString());
                _isBusy = false;

                //throw;
            }
        }

        public void ManualPrompt()
        {
            try
            {
                PrepareForms();
            }
            catch (Exception e)
            {
                Logger.Instance.Log(e.ToString());
                _isBusy = false;
                
                //throw;
            }
        }

        #region ActivityCheck

        private void ActivityCheck()
        {
            TimeSpan elapsedTime = TimeSpan.FromMilliseconds(IdleTimeFinder.GetIdleTime());

            if (elapsedTime.TotalMilliseconds < _msTrigger)
                ActivityOccured?.Invoke();
        }

        public static event Action ActivityOccured;

        #endregion

        #region Forms Check

        private void PrepareForms()
        {
            _isBusy = true;
            Logger.Instance.Log("PREPARE FORMS...");

            NewFormsRequested?.Invoke(_lastPrompt.AddMilliseconds(1), DateTime.Now);
           

            _lastPrompt = DateTime.Now;
            _isBusy = false;
        }

        /// <summary>
        /// Global Timer is notifying all subscribers to create forms about the requested timespan.
        /// </summary>
        public static event Action<DateTime, DateTime> NewFormsRequested;

        #endregion

        #region Foreground App Check


        private ForegroundApp _current = new ForegroundApp();

        private void ForegroundAppUpdate()
        {
            var allwindows = SystemWindow.AllToplevelWindows.Select(ExtractAppData).ToArray();
            ForegroundAppsDiscovered?.Invoke(allwindows);

            ForegroundApp a = ExtractAppData(SystemWindow.ForegroundWindow);

            if (!_current.Equals(a))
            {
                _current.Status = AppStatus.Deactivated;

                ForegroundAppChanged?.Invoke(_current, a);
                _current = a;
            }
        }

        private static ForegroundApp ExtractAppData(SystemWindow foreground)
        {
            return new ForegroundApp
            {
                WindowTitle = foreground.Title,
                ProcessHandle = foreground.Process.Id.ToString(),
                ProcessName = foreground.Process.ProcessName,
                ProcessPath = foreground.Process.StartInfo.FileName,
                Status = AppStatus.Activated
            };
        }

        /// <summary>
        /// This event notifies of a change in foreground app. Arg1: Old Foreground App, Arg2: New Foreground App
        /// </summary>
        public static event Action<ForegroundApp, ForegroundApp> ForegroundAppChanged;

        /// <summary>
        /// This event notifies of the current foreground apps.
        /// </summary>
        public static event Action<ForegroundApp[]> ForegroundAppsDiscovered;

        #endregion

        #region Processes Check

        private void ProcessesUpdate()
        {
            ProcessesDiscovered?.Invoke();
        }

        public static event Action ProcessesDiscovered;

        #endregion

        
        private void OnClipboardChanged(object sender, EventArgs eventArgs)
        {
            if (!_isEnabled) return;
        }

        private void KeyboardOnOnKeyPressed(object sender, KeyPressedArgs k)
        {
            if (!_isEnabled) return;

            Key key = KeyInterop.KeyFromVirtualKey(k.KeyCode);

            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0 && key == Key.S)
            {
                // Saving
            }
        }
    }
}
