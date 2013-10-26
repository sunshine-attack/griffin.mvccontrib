using System;
using System.Reflection;

namespace SunshineAttack.Localization.VirtualPathProvider
{
    /// <summary>
    ///   Maps a namespace to a virtual path
    /// </summary>
    public class NamespaceMapping
    {
        private readonly Assembly _assembly;
        private readonly string _folderNamespace;

        /// <summary>
        ///   Initializes a new instance of the <see cref="NamespaceMapping" /> class.
        /// </summary>
        /// <param name="assembly"> The assembly that the views are located in. </param>
        /// <param name="folderNamespace"> Namespace that should correspond to the virtual path "~/". Typically root namespace in your project.</param>
        /// <example>
        /// HostingEnvironment.RegisterVirtualPathProvider(SunshineAttackVirtualPathProvider.Current);
        /// 
        /// var embeddedProvider = new EmbeddedViewFileProvider();
        /// embeddedProvider.Add(new NamespaceMapping(typeof (SunshineAttackHomeController).Assembly, "Griffin.MvcContrib"));
        /// 
        /// SunshineAttackVirtualPathProvider.Current.Add(embeddedProvider);
        /// </example>
        public NamespaceMapping(Assembly assembly, string folderNamespace)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (folderNamespace == null) throw new ArgumentNullException("folderNamespace");

            _assembly = assembly;
            _folderNamespace = folderNamespace;
        }

        /// <summary>
        ///   Gets assembly that the embedded views are located in
        /// </summary>
        public Assembly Assembly
        {
            get { return _assembly; }
        }

        /// <summary>
        ///   Gets namespace that corresponds to application root
        /// </summary>
        public string FolderNamespace
        {
            get { return _folderNamespace; }
        }
    }
}