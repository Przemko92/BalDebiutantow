using System;
using System.Threading.Tasks;
using EventsSourcing;
using EventsSourcing.Configs;
using EventsSourcing.Helpers;

namespace ReportingService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Task.Run(() => ConnectToRabbitMq());

            Console.ReadLine();
        }

        private static void ConnectToRabbitMq()
        {
            var config = new RabbitMqConfig(
                new RabbitMqInstance[] { new RabbitMqInstance("127.0.0.1", 5672) },
                "user",
                "password",
                new[] { "UsersEvents", "SystemEvents" });

            var rabbitMqConnector = new RabbitMqConnector(config, new BasicJsonSerializer());
            rabbitMqConnector.NewMessage += RabbitMqConnectorOnNewMessage;
            rabbitMqConnector.Initialize();
        }

        private static void RabbitMqConnectorOnNewMessage(object sender, object e)
        {
            Console.WriteLine("ReportingService " + e);
        }
    }
}
