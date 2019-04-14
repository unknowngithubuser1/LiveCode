using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using KaVE.Commons.Model.Events;
using KaVE.Commons.Model.Events.TestRunEvents;
using KaVE.Commons.Utils.Json;
using KaVEData.Tools;
using livecode.Common.Events;
using livecode.ComponentBase.AppPoint;
using livecode.ComponentBase.FilePoint;
using livecode.wpf.Logs;
using BuildEvent = KaVE.Commons.Model.Events.VisualStudio.BuildEvent;
using CommandEvent = KaVE.Commons.Model.Events.CommandEvent;
using DebuggerEvent = KaVE.Commons.Model.Events.VisualStudio.DebuggerEvent;
using DocumentEvent = KaVE.Commons.Model.Events.VisualStudio.DocumentEvent;
using EditEvent = KaVE.Commons.Model.Events.VisualStudio.EditEvent;
using ErrorEvent = KaVE.Commons.Model.Events.ErrorEvent;
using IDEStateEvent = KaVE.Commons.Model.Events.VisualStudio.IDEStateEvent;
using InfoEvent = KaVE.Commons.Model.Events.InfoEvent;
using NavigationEvent = KaVE.Commons.Model.Events.NavigationEvent;
using SolutionEvent = KaVE.Commons.Model.Events.VisualStudio.SolutionEvent;
using SystemEvent = KaVE.Commons.Model.Events.SystemEvent;
using VersionControlEvent = KaVE.Commons.Model.Events.VersionControlEvents.VersionControlEvent;
using WindowEvent = KaVE.Commons.Model.Events.VisualStudio.WindowEvent;

namespace livecode.wpf.Components.Logs
{
    public class DBInsertAgent
    {
        #region Singleton

        private static DBInsertAgent _instance;
        private static object _lock = true;

        public static DBInsertAgent Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new DBInsertAgent());
                }
            }
        }

        public static void Initialize()
        {
            if (_instance == null) _instance = new DBInsertAgent();
        }

        #endregion

        private DBInsertAgent()
        {
            
        }

        private KaVEEntities _baseContext = new KaVEEntities();
        private Dictionary<Session, int> _sessions = new Dictionary<Session, int>();

        public void Visit(IDEEvent e)
        {
            try
            {
                if (_baseContext.Events.Max(v => v.TriggeredAt) > e.TriggeredAt)
                    return;
            }
            catch (Exception exception)
            {
                Logger.Instance.Log(exception.ToString());
                return;
            }

            if (e is CommandEvent) process((CommandEvent)e);
            else if (e is DocumentEvent) process((DocumentEvent)e);
            else if (e is InfoEvent) process((InfoEvent)e);
            else if (e is SystemEvent) process((SystemEvent)e);
            else if (e is WindowEvent) process((WindowEvent)e);
            else if (e is VersionControlEvent) process((VersionControlEvent)e);
            else if (e is SolutionEvent) process((SolutionEvent)e);
            else if (e is NavigationEvent) process((NavigationEvent)e);
            else if (e is ErrorEvent) process((ErrorEvent)e);
            else if (e is EditEvent) process((EditEvent)e);
            else if (e is TestRunEvent) process((TestRunEvent)e);
            else if (e is IDEStateEvent) process((IDEStateEvent)e);
            else if (e is DebuggerEvent) process((DebuggerEvent)e);
            else if (e is BuildEvent) process((BuildEvent)e);
            else processBasic(e);

        }

        public void Visit(KaVEData.Tools.ActivityEvent e)
        {
            var session = FindSession();

            e.Session = session;
            e.SequenceId = ++_sessions[session];
            e.Type = e.GetType().Name;
            e.TriggeredAt = DateTime.Now;
            e.TimeStamp = (long)e.TriggeredAt.Value.Subtract(new DateTime(2010, 01, 01)).TotalMilliseconds;
            e.TriggeredBy = "OS Interaction";
            e.Duration = 1000.ToString();
            e.ActiveDocumentName = "";
            e.ActiveDocumentType = "";
            e.ActiveWindowCaption = "";
            e.ActiveWindowType = "";

            e.Details = "";

            session.Events.Add(e);

            _baseContext.Events.Add(e);

            SaveChanges();
        }

        public void Visit(ComponentBase.FilePoint.CodeFile result)
        {
            var e = new CodeEvent();

            var session = FindSession();

            e.Session = session;
            e.SequenceId = ++_sessions[session];
            e.Type = e.GetType().Name;
            e.TriggeredAt = result.LastEdit;
            e.TimeStamp = (long)e.TriggeredAt.Value.Subtract(new DateTime(2010, 01, 01)).TotalMilliseconds;
            e.TriggeredBy = "Save File";
            e.Duration = 1000.ToString();
            e.ActiveDocumentName = result.Name ?? "";
            e.ActiveDocumentType = result.Extension;
            e.ActiveWindowCaption = "";
            e.ActiveWindowType = "";

            e.Details = "Code Change // " + result.FullPath;
            e.Content = result.Changes.LastOrDefault()?.Content ?? "-";
            e.Exists = result.Exists;
            e.FullPath = result.FullPath;

            session.Events.Add(e);

            _baseContext.Events.Add(e);

            SaveChanges();
        }

        public void Visit(ForegroundApp result, string type, bool isActive=false)
        {
            var e = new AppEvent();

            var session = FindSession();

            e.Session = session;
            e.SequenceId = ++_sessions[session];
            e.Type = e.GetType().Name;
            e.TriggeredAt = result.TriggeredAt;
            e.TimeStamp = (long)e.TriggeredAt.Value.Subtract(new DateTime(2010, 01, 01)).TotalMilliseconds;
            e.TriggeredBy = isActive ? type : "-";
            e.Duration = 1000.ToString();
            e.ActiveDocumentName = "";
            e.ActiveDocumentType = "";
            e.ActiveWindowCaption = result.WindowTitle;
            e.ActiveWindowType = "";

            e.Details = result.Details;
            e.AppType = type;
            e.ProcessHandle = result.ProcessHandle;
            e.ProcessName = result.ProcessName;
            e.ProcessPath = result.ProcessPath;
            e.WindowTitle = result.WindowTitle;
            e.IsActive = isActive;

            session.Events.Add(e);

            _baseContext.Events.Add(e);

            SaveChanges();
        }

        public void Visit(Process process, string cmd, AppType type)
        {
            var e = new AppEvent();

            var session = FindSession();

            e.Session = session;
            e.SequenceId = ++_sessions[session];
            e.Type = e.GetType().Name;
            e.TriggeredAt = DateTime.Now;
            e.TimeStamp = (long)e.TriggeredAt.Value.Subtract(new DateTime(2010, 01, 01)).TotalMilliseconds;
            e.TriggeredBy = "-";
            e.Duration = 1000.ToString();
            e.ActiveDocumentName = "";
            e.ActiveDocumentType = "";
            e.ActiveWindowCaption = process.MainWindowTitle;
            e.ActiveWindowType = "";

            e.Details = process.ProcessName + " : " + process.StartInfo.FileName;
            e.AppType = type.ToString();
            e.ProcessHandle = process.Handle.ToString();
            e.ProcessName = process.ProcessName;
            e.ProcessPath = process.StartInfo.FileName;
            e.WindowTitle = process.MainWindowTitle;
            e.IsActive = true;

            session.Events.Add(e);

            _baseContext.Events.Add(e);

            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _baseContext.Database.CommandTimeout = 1000000000;
            _baseContext.SaveChanges();
        }

        #region KaVE

        private void processBasic(IDEEvent e)
        {
            object t = new Event();
            var ce = CreateEventData<Event>(e, ref t);

            _baseContext.Events.Add(ce);
        }

        private void process(CommandEvent e)
        {
            if (e.CommandId != null)
            {
                if (e.CommandId.Contains("View.ObjectBrowsingScope") || e.CommandId.Contains("Debug.Startup") || e.CommandId.Contains("ChooseLanguage"))
                    return;

                if (e.CommandId.Contains(":"))
                    e.CommandId = e.CommandId.Substring(e.CommandId.LastIndexOf(":", StringComparison.Ordinal) + 1);
            }
            else
            {
                return;
            }

            object t = new KaVEData.Tools.CommandEvent();
            var ce = CreateEventData<KaVEData.Tools.CommandEvent>(e, ref t);

            ce.Details = e.CommandId;
            ce.CommandId = e.CommandId;
        }

        private void process(DocumentEvent e)
        {
            object t = new KaVEData.Tools.DocumentEvent();
            var ce = CreateEventData<KaVEData.Tools.DocumentEvent>(e, ref t);

            ce.DocumentAction = e.Action.ToString("G");
            ce.DocumentName = e.Document?.FileName ?? "";
            ce.Details = ce.DocumentName + "\\" + ce.DocumentAction;

            _baseContext.Events.Add(ce);
        }

        private void process(ErrorEvent e)
        {
            object t = new KaVEData.Tools.ErrorEvent();
            var ce = CreateEventData<KaVEData.Tools.ErrorEvent>(e, ref t);

            ce.ErrorContent = e.Content;
            ce.Details = ce.ErrorContent;

            _baseContext.Events.Add(ce);
        }

        private void process(NavigationEvent e)
        {
            object t = new KaVEData.Tools.NavigationEvent();
            var ce = CreateEventData<KaVEData.Tools.NavigationEvent>(e, ref t);

            ce.Location = e.Location.Identifier;
            ce.NavigationType = e.TypeOfNavigation.ToString("G");
            ce.Target = e.Target.ToString();
            ce.Details = ce.Location + "\\" + ce.NavigationType + "\\" + ce.Target;

            _baseContext.Events.Add(ce);
        }

        private void process(EditEvent e)
        {
            string enclosingTypeName;

            if (e.Context2 == null)
                return;
            else
            {
                var snapshotOfEnclosingType = e.Context2.SST;
                enclosingTypeName = snapshotOfEnclosingType.EnclosingType.FullName;
            }

            if (enclosingTypeName == "?")
                return;

            object t = new KaVEData.Tools.EditEvent();
            var ce = CreateEventData<KaVEData.Tools.EditEvent>(e, ref t);

            ce.NumberOfChanges = e.NumberOfChanges;
            ce.SizeOfChanges = e.SizeOfChanges;
            ce.TypeName = enclosingTypeName;
            ce.Details = enclosingTypeName;
            ce.Context = e.Context2.SST.ToFormattedJson();

            _baseContext.Events.Add(ce);
        }

        private void process(VersionControlEvent e)
        {
            foreach (var ac in e.Actions)
            {
                object t = new KaVEData.Tools.VersionControlEvent();
                var ce = CreateEventData<KaVEData.Tools.VersionControlEvent>(e, ref t);

                ce.Details = e.Solution.Identifier + "\\" + ac.ActionType.ToString("G");
                ce.TriggeredAt = ac.ExecutedAt;
                ce.Solution = e.Solution.Identifier;
                ce.ActionType = ac.ActionType.ToString("G");

                _baseContext.Events.Add(ce);
            }
        }

        private void process(WindowEvent e)
        {
            object t = new KaVEData.Tools.WindowEvent();
            var ce = CreateEventData<KaVEData.Tools.WindowEvent>(e, ref t);

            ce.Window = e.Window.Caption;
            ce.Action = e.Action.ToString("G");
            ce.Details = ce.Window + "\\" + ce.Action;

            _baseContext.Events.Add(ce);
        }

        private void process(InfoEvent e)
        {
            object t = new KaVEData.Tools.InfoEvent();
            var ce = CreateEventData<KaVEData.Tools.InfoEvent>(e, ref t);

            ce.InfoContent = e.Info;

            _baseContext.Events.Add(ce);
        }

        private void process(SolutionEvent e)
        {
            object t = new KaVEData.Tools.SolutionEvent();
            var ce = CreateEventData<KaVEData.Tools.SolutionEvent>(e, ref t);

            ce.Action = e.Action.ToString("G");
            ce.Target = e.Target.Identifier;
            ce.Details = ce.Target + "\\" + ce.Action;

            _baseContext.Events.Add(ce);
        }

        private void process(TestRunEvent e)
        {
            foreach (var tr in e.Tests)
            {
                object t = new TestEvent();
                var te = CreateEventData<TestEvent>(e, ref t);

                te.Details = tr.TestMethod.FullName;
                te.MethodName = te.Details;
                te.Result = tr.Result.ToString("G");
                te.TestStatus = te.Result;
                te.TriggeredAt = tr.StartTime ?? te.TriggeredAt;
                te.Duration = tr.Duration.TotalMilliseconds.ToString();

                _baseContext.Events.Add(te);
            }
        }

        private void process(IDEStateEvent e)
        {
            object t = new KaVEData.Tools.IDEStateEvent();
            var ce = CreateEventData<KaVEData.Tools.IDEStateEvent>(e, ref t);

            ce.Details = e.IDELifecyclePhase.ToString("G");
            ce.Phase = ce.Details;

            if (e.OpenDocuments != null && e.OpenDocuments.Count > 0)
                ce.OpenDocuments = string.Join(",", e.OpenDocuments.Select(d => d.FileName));
            
            _baseContext.Events.Add(ce);
        }

        private void process(DebuggerEvent e)
        {
            object t = new KaVEData.Tools.DebuggerEvent();
            var ce = CreateEventData<KaVEData.Tools.DebuggerEvent>(e, ref t);

            ce.Action = e.Action;
            ce.Reason = e.Reason;
            ce.Details = e.Action + "\\" + e.Reason;
            ce.Result = e.Mode.ToString("G");

            _baseContext.Events.Add(ce);
        }

        private void process(SystemEvent e)
        {
            object t = new KaVEData.Tools.SystemEvent();
            var ce = CreateEventData<KaVEData.Tools.SystemEvent>(e, ref t);

            ce.Action = e.Type.ToString("G");
            ce.Details = ce.Action;

            _baseContext.Events.Add(ce);
        }

        private void process(BuildEvent e)
        {
            foreach (var te in e.Targets)
            {
                object t = new KaVEData.Tools.BuildEvent();
                var ce = CreateEventData<KaVEData.Tools.BuildEvent>(e, ref t);

                ce.Details = e.Action + "\\" + te.Project;
                ce.Action = e.Action;
                ce.BuildStatus = te.Successful;
                ce.Project = te.Project;

                ce.Result = te.Successful ? "Successful" : "Fail";
                ce.TriggeredAt = te.StartedAt ?? ce.TriggeredAt;
                ce.Duration = te.Duration?.TotalMilliseconds.ToString();

                _baseContext.Events.Add(ce);
            }

        }

        private T CreateEventData<T>(IDEEvent ie, ref object e) where T : class
        {
            var je = e as Event;

            var session = FindSession(ie);

            je.Session = session;
            je.SequenceId = ++_sessions[session];
            je.Type = ie.GetType().Name;
            je.TriggeredAt = ie.TriggeredAt ?? SqlDateTime.MinValue.Value;
            je.TimeStamp = (long)je.TriggeredAt.Value.Subtract(new DateTime(2010,01,01)).TotalMilliseconds;
            je.TriggeredBy = ie.TriggeredBy.ToString("G");
            je.Duration = ie.Duration?.TotalMilliseconds.ToString();
            je.ActiveDocumentName = ie.ActiveDocument?.FileName ?? "";
            je.ActiveDocumentType = ie.ActiveDocument?.Language;
            je.ActiveWindowCaption = ie.ActiveWindow?.Caption;
            je.ActiveWindowType = ie.ActiveWindow?.Type;

            session.Events.Add(je);

            return je as T;
        }

        private Session FindSession(IDEEvent e)
        {
            var session = _sessions.Select(p => p.Key).FirstOrDefault(s => s.IDESessionId == e.IDESessionUUID);

            if (session == null)
            {
                session = createSession(e);
            }
            return session;
        }

        private Session FindSession()
        {
            var session = _sessions.LastOrDefault().Key ?? createSession();

            return session;
        }

        private Session createSession(IDEEvent e)
        {
            Session s = new Session { IDESessionId = e.IDESessionUUID };
            _sessions.Add(s, 0);

            return s;
        }

        private Session createSession()
        {
            Session s = new Session { IDESessionId = Guid.NewGuid().ToString("B") };
            _sessions.Add(s, 0);

            return s;
        }


        #endregion
    }
}
