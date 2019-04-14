using livecode.ComponentBase.FilePoint;

namespace livecode.ComponentBase.Measures
{
    /// <summary>
    /// PSP Size Measurements based on Watts S. Humphrey's PSP
    /// </summary>
    public class PSPSizeMeasure : IMeasure
    {
        public int Base { get; set; }

        public int Added { get; set; }

        public int Modified { get; set; }

        public int Deleted { get; set; }

        public int NewAndChanged => Added + Modified;

        public int Reused { get; set; }

        public int NewReuse { get; set; }

        public int Total { get; set; }
    }
}
