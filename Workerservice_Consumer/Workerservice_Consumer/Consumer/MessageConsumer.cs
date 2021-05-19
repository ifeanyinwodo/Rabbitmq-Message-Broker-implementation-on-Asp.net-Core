using ItemModel_Nugget;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Workerservice_Consumer.Broker;

namespace Workerservice_Consumer.Consumer
{
    public class MessageConsumer 
    {
        private readonly RabbitMQOptions _rabbitMQOptions;
        public MessageConsumer(RabbitMQOptions rabbitMQOptions)
        {
            _rabbitMQOptions = rabbitMQOptions;
        }

        public Item ReceiveMessage()
        {
            Item item = null;
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

               var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                 {
                     var body = ea.Body;
                     var message = Encoding.UTF8.GetString(body.ToArray());
                     item = JsonConvert.DeserializeObject<Item>(message);
                     channel.BasicAck(deliveryTag: ea.DeliveryTag, false);
                     Thread.Sleep(1000);

                 };

                channel.BasicConsume(_rabbitMQOptions.RabbitMQQueue, false, consumer);
                Thread.Sleep(1000);
                return item;
            }
        }
    }
}
