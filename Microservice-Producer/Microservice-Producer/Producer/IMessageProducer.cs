using ItemModel_Nugget;

namespace Microservice_Producer.Producer
{
    public interface IMessageProducer
    {
        bool PublishMessage(Item item);
    }
}