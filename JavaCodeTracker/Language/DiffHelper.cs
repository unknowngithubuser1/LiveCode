using System.Linq;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using livecode.ComponentBase.FilePoint;

namespace livecode.JavaCodeTracker.Language
{
    public class DiffHelper
    {
        public static ChangeType[] SimpleDiff(string current, string old)
        {
            IDiffer differ = new Differ();
            SideBySideDiffBuilder builder = new SideBySideDiffBuilder(differ);

            var diffModel = builder.BuildDiffModel(old, current);
            ClassifyCodeChangeType(current, old);
            return diffModel.NewText.Lines.Select(l => l.Type).ToArray();
        }

        public static CodeChangeType ClassifyCodeChangeType(string current, string old)
        {
            IDiffer differ = new Differ();
            SideBySideDiffBuilder builder = new SideBySideDiffBuilder(differ);

            var diffModel = builder.BuildDiffModel(old, current);

            CodeChangeType t = CodeChangeType.None;
            
            for (var i = 0; i < diffModel.NewText.Lines.Count; i++)
            {
                var newLine = diffModel.NewText.Lines[i];
                var oldLine = diffModel.OldText.Lines[i];

                if (newLine.Type == ChangeType.Inserted)
                {
                    t |= ChangeHelper.InsertedLineType(newLine);
                }
                else if (newLine.Type == ChangeType.Modified)
                {
                    t |= ChangeHelper.ModifiedLineType(oldLine, newLine);
                }
                else if (oldLine.Type == ChangeType.Deleted)
                {
                    t |= ChangeHelper.RemovedLineType(oldLine);
                }

            }
            
            return t;
        }
    }
}
