using System.Threading.Tasks;

namespace livecode.wpf.Util
{
    /// <summary>
    /// An operator that ONLY downloads and returns data asynchronously.
    /// It should only report errors and notifications to a static class and not return them.
    /// </summary>
    /// <typeparam name="T">Type of the downloaded data</typeparam>
    public interface IDownloadOperator<T>
    {
        Task<T> Download();
    }
}
