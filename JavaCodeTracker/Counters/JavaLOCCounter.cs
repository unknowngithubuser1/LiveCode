using System;
using System.Linq;
using livecode.ComponentBase.FilePoint;
using livecode.ComponentBase.Measures;

namespace livecode.JavaCodeTracker.Counters
{
    public class JavaLOCCounter : ICounter
    {
        /// <summary>
        /// Physical lines of code, including whitespaces.
        /// </summary>
        /// <returns></returns>
        public static int GetPhysicalLOC(string[] codeLines)
        {
            return codeLines.Length;
        }

        /// <summary>
        /// Physical lines of code, excluding whitespaces.
        /// </summary>
        /// <returns></returns>
        public static int GetNonWhitespaceLOC(string[] codeLines)
        {
            return codeLines.Count(l => !string.IsNullOrWhiteSpace(l));
        }

        /// <summary>
        /// Lines of code that contain comments. Note: multi-line comments are considered as one comment line.
        /// </summary>
        /// <param name="codeLines"></param>
        /// <returns></returns>
        public static int GetCommentLOC(string[] codeLines)
        {
            return codeLines.Count(l => l.Contains("//") || l.Contains("/*"));
        }

        /// <summary>
        /// Lines of codes that require some minimum amount of effort.
        /// </summary>
        /// <param name="codeLines"></param>
        /// <returns></returns>
        public static int GetStatementLOC(string[] codeLines)
        {
            return codeLines.Count(l => 
                                        !l.StartsWith("import ") &&
                                        !l.StartsWith("package ") &&
                                        !l.StartsWith("//") &&
                                        !l.StartsWith("/*") &&
                                        l != "{" &&
                                        l != "}" &&
                                        l.Length > 5);
        }

        /// <summary>
        /// Lines of code that serve an algorithmic function
        /// </summary>
        /// <param name="codeLines"></param>
        /// <returns></returns>
        public static int GetStructuralLOC(string[] codeLines)
        {
            return codeLines.Count(l => 
                                        l.StartsWith("if ") ||
                                        l.StartsWith("if(") ||
                                        l.StartsWith("if(") ||
                                        l.StartsWith("else ") ||
                                        l.StartsWith("while ") ||
                                        l.StartsWith("while(") ||
                                        l.StartsWith("for ") ||
                                        l.StartsWith("for(")
                                        );
        }

        public static LOCMeasure GetLocMeasure(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return new LOCMeasure();

            string[] codeLines = code.Replace("\r", "").Split('\n').Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Trim(' ', '\t')).ToArray();

            if (codeLines.Length == 0)
                return new LOCMeasure();

            LOCMeasure m = new LOCMeasure()
            {
                PhysicalLOC = GetPhysicalLOC(codeLines),
                NonWhitespaceLOC = GetNonWhitespaceLOC(codeLines),
                CommentLOC = GetCommentLOC(codeLines),
                StatementLOC = GetStatementLOC(codeLines),
                StructuralLOC = GetStructuralLOC(codeLines),
                AverageLineLength = (int)codeLines.Average(l => l.Length)
            };

            return m;
        }

        public Type MeasureType => typeof(LOCMeasure);

        public IMeasure Measure(CodeHistory history)
        {
            return GetLocMeasure(history.Content);
        }
    }
}
