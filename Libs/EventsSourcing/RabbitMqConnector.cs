using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EventsSourcing.Configs;
using EventsSourcing.Helpers;
using EventsSourcing.Helpers.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventsSourcing
{
    public class RabbitMqConnector
    {
        public event EventHandler<object> NewMessage;
        private readonly RabbitMqConfig _config;
        private readonly IMessagesSerializer _serializer;
        private string _appName;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqConnector(RabbitMqConfig config, IMessagesSerializer serializer)
        {
            this._appName = Assembly.GetEntryAssembly().GetName().Name;
            this._config = config;
            this._serializer = serializer;
        }

        public void Initialize()
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.AutomaticRecoveryEnabled = true;
            connectionFactory.TopologyRecoveryEnabled = false;
            connectionFactory.UserName = this._config.Username;
            connectionFactory.Password = this._config.Password;

            this._connection = connectionFactory.CreateConnection(
                _config.MqInstances.Select(x => new AmqpTcpEndpoint(x.HostName, x.Port)).ToList());

            //Tu możemy się podpiąć pod eventy
            //this._connection.ConnectionShutdown += ConnectionOnConnectionShutdown;
            //this._connection.RecoverySucceeded += ConnectionOnRecoverySucceeded;
            //this._connection.ConnectionRecoveryError += ConnectionOnConnectionRecoveryError;
            this._channel = _connection.CreateModel();
            this._channel.ExchangeDeclare(_appName, "direct", false, false);
            this._channel.QueueDeclare(_appName, false, false, false, null);
            this._channel.QueueBind(_appName, _appName, "");

            foreach (var exchange in _config.Exchanges)
            {
                Console.WriteLine("Binding to exchange {0}", exchange);
                this._channel.ExchangeDeclare(exchange, "direct", false, false);
                this._channel.QueueBind(_appName, exchange, "");
            }

            CreateConsumer();
        }

        private void CreateConsumer()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += ConsumerOnReceived;
            //Tu możemy się podpiąć pod eventy, które mogę się przydać
            //consumer.ConsumerCancelled += ConsumerOnConsumerCancelled;
            //consumer.Registered += ConsumerOnRegistered;
            //consumer.Unregistered += ConsumerOnUnregistered;
            _channel.BasicConsume(consumer, _appName, autoAck: false);
        }

        public void Send(object item, string queueName)
        {
            Console.WriteLine("Sending {0} bytes to {1}", item, queueName);
            DataChunk serialized = this._serializer.Serialize(item);

            var properties = _channel.CreateBasicProperties();
            properties.ContentType = serialized.ContentType;
            properties.AppId = _appName;

            _channel.BasicPublish(
                exchange: queueName,
                routingKey: "",
                basicProperties: properties,
                body: serialized.Body);
        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine("Received {0} bytes from Queue:{1} AppId:{2}", e.Body.Length, e.Exchange, e.BasicProperties.AppId);
            var deserialized = this._serializer.Deserialize(new DataChunk(e.Body,e.BasicProperties.ContentType));
            NewMessage?.Invoke(this, deserialized);
            this._channel.BasicAck(e.DeliveryTag, false);
        }
    }
}