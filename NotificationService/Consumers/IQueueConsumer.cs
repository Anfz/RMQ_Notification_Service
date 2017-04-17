using RabbitMQ.Client;

namespace NotificationService
{
    public interface IQueueConsumer
    {

        void CreateConnection();

        void ProcessMessages(); 
    }
}