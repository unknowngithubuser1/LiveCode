using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livecode.Common.Events
{

    public class BackgroundProcess : EventBase, ICloneable
    {
        public string ProcessHandle { get; set; }

        public string ProcessName { get; set; }

        public string ProcessPath { get; set; }

        public string CommandLine { get; set; }
        
        public override string Details { get { return $"Process : {ProcessName} ({ProcessHandle})"; } }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ForegroundApp))
                return false;

            var other = obj as ForegroundApp;

            return string.Equals(ProcessHandle, other.ProcessHandle) &&
                   string.Equals(ProcessName, other.ProcessName) && string.Equals(ProcessPath, other.ProcessPath);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ProcessHandle != null ? ProcessHandle.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProcessName != null ? ProcessName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProcessPath != null ? ProcessPath.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
