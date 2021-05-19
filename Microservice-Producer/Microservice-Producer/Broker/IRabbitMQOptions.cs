namespace Microservice_Producer.Broker
{
    public interface IRabbitMQOptions
    {
        string RabbitMQAppRouteKey { get; }
        string RabbitMQExchange { get; }
        string RabbitMQExchangeType { get; }
        string RabbitMQHostname { get; }
        string RabbitMQLogRouteKey { get; }
        string RabbitMQPassword { get; }
        string RabbitMQPort { get; }
        string RabbitMQQueue { get; }
        string RabbitMQUsername { get; }
    }
}