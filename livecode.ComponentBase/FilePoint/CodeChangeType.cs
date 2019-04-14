using System;

namespace livecode.ComponentBase.FilePoint
{
    [Flags]
    public enum CodeChangeType
    {
        None = 0,
        Documentation = 1,
        Syntax = 2,
        Assignment = 4,
        Interface = 8,
        Checking = 16,
        Data = 32,
        Function = 64
    }

    [Flags]
    public enum CodeChangeReason
    {
        None = 0,
        Education = 1,
        Communication = 2,
        Oversight = 4,
        Transcription = 8,
        Process = 16
    }
}