using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using URLShortener.Infrastructure.AppContext;

namespace URLShortener.Infrastructure
{
    public static class NhibernateRegistration
    {
        public static IServiceCollection AddNhibernate(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MariaDbConnection");
            var sessionFactory = NHibernateConfig.CreateSessionFactory(connectionString);

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }
    }
}
