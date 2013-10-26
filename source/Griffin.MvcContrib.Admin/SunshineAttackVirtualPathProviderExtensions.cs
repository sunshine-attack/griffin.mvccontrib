using System;
using System.Web;
using SunshineAttack.Localization.Areas.SunshineAttack;
using SunshineAttack.Localization.VirtualPathProvider;

namespace SunshineAttack.Localization
{
    /// <summary>
    /// Extension to make the configuration easier.
    /// </summary>
    public static class SunshineAttackVirtualPathProviderExtensions
    {
        /// <summary>
        /// Register the content files used by the adminstration area.
        /// </summary>
        /// <param name="provider"><c>SunshineAttackVirtualPathProvider.Current</c></param>
        /// <param name="layoutVirtualPath">Typically <c>"~/Views/Shared/_Layout.cshtml"</c></param>
        public static void RegisterAdminFiles(this SunshineAttackVirtualPathProvider provider, string layoutVirtualPath)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            if (layoutVirtualPath == null) throw new ArgumentNullException("layoutVirtualPath");
            // you can assign a custom SunshineAttackWebViewPage or a custom layout in EmbeddedViewFixer.
            var fixer = new ExternalViewFixer()
            {
                LayoutPath = layoutVirtualPath
            };

            var sunshineAttackAssembly = typeof(SunshineAttackAreaRegistration).Assembly;

            // for view files
            var embeddedViews = new EmbeddedViewFileProvider(VirtualPathUtility.ToAbsolute("~/"), fixer);
            embeddedViews.Add(new NamespaceMapping(sunshineAttackAssembly, "SunshineAttack.Localization"));
            provider.Add(embeddedViews);
            
            // Add support for loading content files:
            var contentFilesProvider = new EmbeddedFileProvider(VirtualPathUtility.ToAbsolute("~/"));
            contentFilesProvider.Add(new NamespaceMapping(sunshineAttackAssembly, "SunshineAttack.Localization"));
            provider.Add(contentFilesProvider);

        }
    }
}
