using System;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Griffin.MvcContrib.Localization;
using Griffin.MvcContrib.Localization.Types;
using Griffin.MvcContrib.Localization.Views;
using Griffin.MvcContrib.VirtualPathProvider;
using Raven.Client;
using Raven.Client.Document;
using IContainer = Autofac.IContainer;

namespace Griffin.MvcContrib.Admin.TestProject
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private IContainer _container;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*staticfile}", new { staticfile = @".*\.(css|js|gif|jpg)(/.*)?" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { typeof(Controllers.HomeController).Namespace }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);


         

            AddSupportForEmbeddedViews();
            SetupLocalizationProviders();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(GetType().Assembly);
            RegisterLocalizationFeaturesInTheContainer(builder);
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        private void SetupLocalizationProviders()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelMetadataProviders.Current = new LocalizedModelMetadataProvider();
            var temp = new LocalizedModelValidatorProvider();

           ModelValidatorProviders.Providers.Add(temp);

        }

        private static void RegisterLocalizationFeaturesInTheContainer(ContainerBuilder builder)
        {


            // add the controllers
            builder.RegisterControllers(typeof(MvcContrib.Areas.Griffin.GriffinAreaRegistration).Assembly);

            // Loads strings from repositories.
            builder.RegisterType<RepositoryStringProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ViewLocalizer>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterInstance(new Document().Store).SingleInstance();
            builder.RegisterType<RavenDb.Localization.TypeLocalizationRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RavenDb.Localization.ViewLocalizationRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
      
        }

        private static void AddSupportForEmbeddedViews()
        {
            GriffinVirtualPathProvider.Current.RegisterAdminFiles("~/Views/Shared/_Layout.cshtml");
            HostingEnvironment.RegisterVirtualPathProvider(GriffinVirtualPathProvider.Current);
        }
    }
}




public class Document 
{
    private static IDocumentStore _store;

    public IDocumentStore Store 
    {
        get
        {
            //Initialize RavenDB Connection Store
            if (_store != null) return _store;

            _store = new DocumentStore 
            {
                Conventions = {IdentityPartsSeparator = "-"},
                ConnectionStringName = "RavenDB"
            };

            _store.Initialize();

            return _store;
        }
    }
}