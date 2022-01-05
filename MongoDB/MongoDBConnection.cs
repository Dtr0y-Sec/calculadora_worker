using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Calculadora_Worker.MongoDB
{
    public class MongoDBConnection
    {
        public IMongoClient MongoClient { get; }

        private static IConfigurationRoot configuration;

        public MongoDBConnection()
        {
            initConfiguration();
            MongoClient = new MongoClient(configuration["MongoDB:ConnectionString"]);
        }
        private static void initConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddInMemoryCollection();
            configuration = builder.Build();
        }
    }
}
