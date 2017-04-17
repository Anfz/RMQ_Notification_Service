using RabbitMQ.Client;
using System;

namespace NotificationService
{
    public class RabbitMQConsumer : IQueueConsumer
    {

        private const string EXCHANGENAME = "com.cwbeaver_exchange";
        private const string CARDPAYMENTQUEUENAME = "NotificationTopic_Queue";

        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        public void CreateConnection()
        {
            throw new NotImplementedException();
        }

        public void ProcessMessages()
        {
            throw new NotImplementedException();
        }
    }
}