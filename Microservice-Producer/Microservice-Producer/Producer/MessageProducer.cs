using ItemModel_Nugget;
using Microservice_Producer.Broker;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice_Producer.Producer
{
    public class MessageProducer : IMessageProducer
    {
       
        private readonly IRabbitMQOptions _rabbitMQOptions;
        public MessageProducer(IRabbitMQOptions rabbitMQOptions)
        {
            _rabbitMQOptions = rabbitMQOptions;  
        }

        public bool PublishMessage(Item item)
        {

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQOptions.RabbitMQHostname,
                UserName = _rabbitMQOptions.RabbitMQUsername,
                Password = _rabbitMQOptions.RabbitMQPassword,
                Port = int.Parse(_rabbitMQOptions.RabbitMQPort)

            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQOptions.RabbitMQQueue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = JsonConvert.SerializeObject(item);
                var messageBytes = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: _rabbitMQOptions.RabbitMQExchange,
                    routingKey: _rabbitMQOptions.RabbitMQAppRouteKey,
                    basicProperties: null,
                    body: messageBytes);

                return true;
            }
        }
    }
}
