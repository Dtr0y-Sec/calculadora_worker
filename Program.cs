using Calculadora_Worker.Listerners;
using Calculadora_Worker.MongoDB;
using Calculator.Worker.Queues.SumOperation;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Calculator.Worker
{
    class Program
    {
        private static IConnection rabbitmqConnection;

        private static IConfigurationRoot configuration;

        private static void initConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddInMemoryCollection();
            configuration = builder.Build();
        }

        private static IConnectionFactory initFactory()
        {
            return new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:Hostname"],
                UserName = configuration["RabbitMQ:Username"],
                Password = configuration["RabbitMQ:Password"],
                VirtualHost = configuration["RabbitMQ:Virtualhost"],
            };
        }
        static void Main(string[] args)
        {
            AsyncMain(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task AsyncMain(string[] args)
        {
            initConfiguration();

            rabbitmqConnection = initFactory().CreateConnection();
            
            try
            {

                new SumOperationListener(rabbitmqConnection, new SumOperationQueue());

                Console.WriteLine("Initializing worker");

                while (true)
                {
                    await Task.Delay(1000);
                }

            }
            catch (Exception ex)
            {

                if (rabbitmqConnection != null)
                {
                    rabbitmqConnection.Dispose();
                }

                Console.WriteLine(ex.ToString());

                Thread.Sleep(60000);
            }
        }
    }
}