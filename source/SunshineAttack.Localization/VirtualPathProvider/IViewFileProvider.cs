using System;
using System.Collections;
using System.Web.Caching;
using System.Web.Hosting;

namespace SunshineAttack.Localization.VirtualPathProvider
{
    /// <summary>
    /// Provides view files to <see cref="SunshineAttackVirtualPathProvider"/>.
    /// </summary>
    public interface IViewFileProvider
    {
        /// <summary>
        /// Checks if a file exits
        /// </summary>
        /// <param name="virtualPath">Virtual path like "~/Views/Home/Index.cshtml"</param>
        /// <returns><c>true</c> if found; otherwise <c>false</c>.</returns>
        bool FileExists(string virtualPath);

        /// <summary>
        /// Creates a cache dependency based on the specified virtual paths
        /// </summary>
        /// <param name="virtualPath">Virtual path like "~/Views/Home/Index.cshtml"</param>
        /// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource </param>
        /// <param name="utcStart">The UTC time at which the virtual resources were read </param>
        /// <returns>A CacheDependency if the file is found and caching should be used; <see cref="NoCache.Instance"/> if caching should be disabled for the file; <c>null</c> if file is not found.</returns>
        CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart);

        /// <summary>
        /// Returns a cache key to use for the specified virtual path
        /// </summary>
        /// <param name="virtualPath">Virtual path like "~/Views/Home/Index.cshtml"</param>
        /// <returns>CacheDependency if found; otherwise <c>false</c>.</returns>
        string GetCacheKey(string virtualPath);

        /// <summary>
        /// Get file hash.
        /// </summary>
        /// <param name="virtualPath">Virtual path like "~/Views/Home/Index.cshtml"</param>
        /// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource </param>
        /// <returns>a new hash each time the file have changed (if file is found); otherwise null</returns>
        string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies);

        /// <summary>
        /// Get the view
        /// </summary>
        /// <param name="virtualPath">Virtual path like "~/Views/Home/Index.cshtml"</param>
        /// <returns>File</returns>
        VirtualFile GetFile(string virtualPath);
    }
}