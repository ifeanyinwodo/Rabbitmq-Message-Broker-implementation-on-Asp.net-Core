using Serilog;
using Serilog.Sinks.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workerservice_Consumer.Broker;

namespace Workerservice_Consumer.Metrics
{
    public class MetricsRegistry 
    {



        private readonly RabbitMQOptions _rabbitMQOptions;

        public MetricsRegistry(RabbitMQOptions rabbitMQOptions)
        {

            _rabbitMQOptions = rabbitMQOptions;
        }
        public Serilog.Core.Logger _receivedMessage()
        {

            var _logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) =>
          {
              clientConfiguration.Username = _rabbitMQOptions.RabbitMQUsername;
              clientConfiguration.Password = _rabbitMQOptions.RabbitMQPassword;
              clientConfiguration.Exchange = _rabbitMQOptions.RabbitMQExchange;
              clientConfiguration.ExchangeType = _rabbitMQOptions.RabbitMQExchangeType;
              clientConfiguration.DeliveryMode = RabbitMQDeliveryMode.Durable;
              clientConfiguration.RouteKey = _rabbitMQOptions.RabbitMQLogRouteKey;
              clientConfiguration.Port = int.Parse(_rabbitMQOptions.RabbitMQPort);
              clientConfiguration.Hostnames.Add(_rabbitMQOptions.RabbitMQHostname);



          })
          .MinimumLevel.Verbose()
          .CreateLogger();


            return _logger;
        }


    }
}
