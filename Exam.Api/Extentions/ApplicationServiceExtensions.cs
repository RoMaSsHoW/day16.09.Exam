using Npgsql;
using System.Data;

namespace Exam.Api.Extentions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            //ConfigureRepositories(services);

            ConfigureDatabase(services, configuration);

            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(provider =>
            {
                return new NpgsqlConnection(configuration.GetConnectionString("PostgresqlDbConnection"));
            });
        }
    }
}
