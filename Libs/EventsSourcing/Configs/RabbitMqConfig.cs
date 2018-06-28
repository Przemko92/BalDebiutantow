using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSourcing.Configs
{
    public class RabbitMqConfig
    {
        public RabbitMqInstance[] MqInstances { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Exchanges { get; set; }

        public RabbitMqConfig(RabbitMqInstance[] mqInstances, string username, string password, string[] exchanges)
        {
            MqInstances = mqInstances;
            Username = username;
            Password = password;
            Exchanges = exchanges;
        }
    }

    public class RabbitMqInstance
    {
        public string HostName { get; set; }
        public int Port { get; set; }

        public RabbitMqInstance(string hostName, int port)
        {
            HostName = hostName;
            Port = port;
        }
    }
}
