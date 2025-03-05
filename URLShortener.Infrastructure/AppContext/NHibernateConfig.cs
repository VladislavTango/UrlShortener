using NHibernate.Mapping.ByCode;
using NHibernate;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Driver.MySqlConnector;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using MySqlConnector;

namespace URLShortener.Infrastructure.AppContext
{
    public static class NHibernateConfig
    {
        public static ISessionFactory CreateSessionFactory(string connectionString)
        {
            CreateDatabaseIfNotExists(connectionString);

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

            var schemaExport = new SchemaExport(configuration);
            schemaExport.Create(false, true); 

            return configuration.BuildSessionFactory();
        }

        private static void CreateDatabaseIfNotExists(string connectionString)
        {
            var builder = new MySqlConnectionStringBuilder(connectionString);
            string databaseName = builder.Database;
            builder.Database = null;

            using (var connection = new MySqlConnection(builder.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
