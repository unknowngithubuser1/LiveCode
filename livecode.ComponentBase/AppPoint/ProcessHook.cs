namespace livecode.ComponentBase.AppPoint
{
    public abstract class ProcessHook
    {
        public string APIKey { get; set; }

        public string ProcessName { get; set; }

        public string CommandLineRegx { get; set; }
        
        public virtual AppType ProcessDiscovered(string processName, string commandline)
        {
            return AppType.None;
        }
    }
}
