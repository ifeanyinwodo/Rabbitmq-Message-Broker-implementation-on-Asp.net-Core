using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice_Producer.Broker;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.RabbitMQ;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;

namespace Microservice_Producer.Metrics
{
    
    public class MetricsRegistry : IMetricsRegistry
    {
       
       

        private readonly IRabbitMQOptions _rabbitMQOptions;
      
        public MetricsRegistry(IRabbitMQOptions rabbitMQOptions)
        {
            
            _rabbitMQOptions = rabbitMQOptions;
        }
        public Serilog.Core.Logger _receivedMessage()
        {
           
      var _logger  = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) => {
        clientConfiguration.Username = _rabbitMQOptions.RabbitMQUsername;
        clientConfiguration.Password = _rabbitMQOptions.RabbitMQPassword;
        clientConfiguration.Exchange = _rabbitMQOptions.RabbitMQExchange;
        clientConfiguration.ExchangeType = _rabbitMQOptions.RabbitMQExchangeType;
        clientConfiguration.DeliveryMode = RabbitMQDeliveryMode.Durable;
        clientConfiguration.RouteKey = _rabbitMQOptions.RabbitMQLogRouteKey;
        clientConfiguration.Port =int.Parse(_rabbitMQOptions.RabbitMQPort);
        clientConfiguration.Hostnames.Add(_rabbitMQOptions.RabbitMQHostname);
        


    })
    .MinimumLevel.Verbose()
    .CreateLogger();
           

            return _logger;
        }

       
    }
}