using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workerservice_Consumer.Broker
{
    public class RabbitMQOptions //: IRabbitMQOptions
    {
        private IConfiguration _configuration;
        private readonly string _rabbitMQHostname;
        private readonly string _rabbitMQPort;
        private readonly string _rabbitMQQueue;
        private readonly string _rabbitMQUsername;
        private readonly string _rabbitMQPassword;
        private readonly string _rabbitMQLogRouteKey;
        private readonly string _rabbitMQAppRouteKey;
        private readonly string _rabbitMQExchange;
        private readonly string _rabbitMQExchangeType;
        public RabbitMQOptions(IConfiguration configuration)
        {
            _configuration = configuration;
            _rabbitMQHostname = _configuration["RabbitMQSink:RABBITMQ_HOSTNAMES"];
            _rabbitMQPort = _configuration["RabbitMQSink:RABBITMQ_PORT"];
            _rabbitMQPassword = _configuration["RabbitMQSink:RABBITMQ_PASSWORD"];
            _rabbitMQUsername = _configuration["RabbitMQSink:RABBITMQ_USER"];
            _rabbitMQQueue = _configuration["RabbitMQSink:RABBITMQ_APPQUEUE"];
            _rabbitMQLogRouteKey = _configuration["RabbitMQSink:RABBITMQ_LOGROUTEKEY"];
            _rabbitMQAppRouteKey = _configuration["RabbitMQSink:RABBITMQ_APPROUTEKEY"];
            _rabbitMQExchange = _configuration["RabbitMQSink:RABBITMQ_EXCHANGE"];
            _rabbitMQExchangeType = _configuration["RabbitMQSink:RABBITMQ_EXCHANGE_TYPE"];
        }


        public string RabbitMQHostname { get { return _rabbitMQHostname; } }
        public string RabbitMQPassword { get { return _rabbitMQPassword; } }
        public string RabbitMQUsername { get { return _rabbitMQUsername; } }
        public string RabbitMQQueue { get { return _rabbitMQQueue; } }
        public string RabbitMQLogRouteKey { get { return _rabbitMQLogRouteKey; } }
        public string RabbitMQAppRouteKey { get { return _rabbitMQAppRouteKey; } }
        public string RabbitMQExchange { get { return _rabbitMQExchange; } }
        public string RabbitMQExchangeType { get { return _rabbitMQExchangeType; } }
        public string RabbitMQPort { get { return _rabbitMQPort; } }
    }
}
