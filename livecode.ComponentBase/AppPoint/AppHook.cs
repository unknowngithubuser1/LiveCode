namespace livecode.ComponentBase.AppPoint
{
    public abstract class AppHook
    {
        public string APIKey { get; set; }

        public string ProcessNameRegx { get; set; }

        public string WindowTitleRegx { get; set; }

        public string[] ChildWindowTitleRegxs { get; set; }

        public virtual void WindowDeactivated(string windowTitle, string processName)
        {
            return;
        }

        public virtual AppType WindowActivated(string windowTitle, string processName)
        {
            return AppType.None;
        }

        public virtual AppType ResolveType(string windowTitle, string processName)
        {
            return AppType.None;
        }
    }
}
