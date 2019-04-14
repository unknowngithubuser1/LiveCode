using livecode.ComponentBase.FilePoint;

namespace livecode.ComponentBase.Measures
{
    public class LOCMeasure : IMeasure
    {
        public int PhysicalLOC { get; set; }

        public int NonWhitespaceLOC { get; set; }

        public int CommentLOC { get; set; }

        public int StatementLOC { get; set; }

        public int StructuralLOC { get; set; }

        public int AverageLineLength { get; set; }
    }
}
