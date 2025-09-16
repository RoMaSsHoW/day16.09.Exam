using Exam.Application.Repositories;
using Exam.Application.Services;
using Exam.Infrastructure.Repositories;
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
            ConfigureRepositories(services);

            ConfigureServices(services);

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

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
