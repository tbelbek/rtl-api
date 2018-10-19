#region usings

using System.Web.Http;
using LightInject;
using RtlAPI.Data;
using RtlAPI.Services;

#endregion

namespace RtlAPI
{
    /// <summary>
    /// Servisler için register ve ioc configration alanı
    /// </summary>
    public static class IoCRegister
    {
        private static ServiceContainer container { get; set; }

        public static void Configure()
        {
            container = new ServiceContainer();
            container.EnableWebApi(new HttpConfiguration());
            container.RegisterControllers();

            container.RegisterApiControllers();

            container.EnableAnnotatedConstructorInjection();
            container.EnableAnnotatedPropertyInjection();
            container.EnablePerWebRequestScope();
            container.EnableWebApi(GlobalConfiguration.Configuration);
            DependencyFactory.SetContainer(container);
            Register();

            container.EnableMvc();
        }

        /// <summary>
        /// The container register method for services.
        /// </summary>
        public static void Register()
        {
            DependencyFactory.Register<ITvShowRepository, TvShowRepository>(LifeTimeProvider());
            DependencyFactory.Register<ICastPersonRepository, CastPersonRepository>(LifeTimeProvider());
            DependencyFactory.Register<IPeopleRepository, PeopleRepository>(LifeTimeProvider());
            DependencyFactory.Register<IDataService, DataService>(LifeTimeProvider());
        }


        /// <summary>
        /// Container içinde register edilmiş classların yaşam ömürlerini belirler.
        /// </summary>
        /// <returns></returns>
        private static ILifetime LifeTimeProvider() => new PerContainerLifetime();
    }
}