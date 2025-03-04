using NHibernate.Mapping.ByCode;
using NHibernate;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Driver.MySqlConnector;
using NHibernate.Dialect;

namespace URLShortener.Infrastructure.AppContext
{
    public static class NHibernateConfig
    {
        public static ISessionFactory CreateSessionFactory(string connectionString)
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<MySQL57Dialect>(); 
                db.Driver<MySqlConnectorDriver>();
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
                db.SchemaAction = SchemaAutoAction.Update; 
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddMapping(mapping);

            return configuration.BuildSessionFactory();
        }
    }
}
