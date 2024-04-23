using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Library.Mongo.Models;

namespace Library.Mongo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            return services;
        }
    }
}
