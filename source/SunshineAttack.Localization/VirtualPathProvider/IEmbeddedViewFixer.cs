using System;
using System.IO;

namespace SunshineAttack.Localization.VirtualPathProvider
{
    /// <summary>
    /// Used to correct embedded views
    /// </summary>
    /// <remarks>The purpose of the class is to allow the embedded views to look exactly as regular views without @inherits or anything like that.</remarks>
    [Obsolete("Use IExternalViewFixer instead. This class is not invoked by the framework anymore")]
    public interface IEmbeddedViewFixer
    {
        /// <summary>
        /// Modify the view
        /// </summary>
        /// <param name="virtualPath">Path to view</param>
        /// <param name="stream">Stream containing the original view</param>
        /// <returns>Stream with modified contents</returns>
        Stream CorrectView(string virtualPath, Stream stream);
    }
}