using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Document;
using SunshineAttack.Localization.Areas.SunshineAttack;
using SunshineAttack.Localization.Localization;
using SunshineAttack.Localization.Localization.Types;
using SunshineAttack.Localization.Localization.Views;
using SunshineAttack.Localization.RavenDb.Localization;
using SunshineAttack.Localization.VirtualPathProvider;

namespace SunshineAttack.Localization.Admin.TestProject
{ // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
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
            builder.RegisterControllers(typeof(SunshineAttackAreaRegistration).Assembly);

            // Loads strings from repositories.
            builder.RegisterType<RepositoryStringProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ViewLocalizer>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterInstance(new Document().Store).SingleInstance();
            builder.RegisterType<TypeLocalizationRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ViewLocalizationRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
      
        }

        private static void AddSupportForEmbeddedViews()
        {
            SunshineAttackVirtualPathProvider.Current.RegisterAdminFiles("~/Views/Shared/_Layout.cshtml");
            HostingEnvironment.RegisterVirtualPathProvider(SunshineAttackVirtualPathProvider.Current);
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
}