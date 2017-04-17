using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Configuration;


namespace NotificationService
{
    public class RabbitMQConsumer : IQueueConsumer
    {

        private const string EXCHANGE_NAME = "com.chriswbeaver_exchange";
        private const string QUEUE_NAME = "NotificationTopic_Queue";

        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        public void CreateConnection()
        {
            _factory = new ConnectionFactory
            {
                HostName = ConfigurationSettings.AppSettings["QueueHostName"].ToString(),
                UserName = ConfigurationSettings.AppSettings["QueueUserName"].ToString(),
                Password = ConfigurationSettings.AppSettings["QueuePassword"].ToString(),
            };

            _connection = _factory.CreateConnection();

            _model = _connection.CreateModel();

            _model.ExchangeDeclare(EXCHANGE_NAME, "topic");

            _model.QueueDeclare(QUEUE_NAME, true, false, false, null);
            _model.QueueBind(QUEUE_NAME, EXCHANGE_NAME, "notification.email");
            _model.BasicQos(0, 1, false);
        }

        public void ProcessMessages()
        {
            Subscription subscription = new Subscription(_model, QUEUE_NAME, false); 

            while (true)
            {
                BasicDeliverEventArgs delArgs = subscription.Next();

                var message = System.Text.Encoding.UTF8.GetString(delArgs.Body);
                Console.WriteLine(message);
                subscription.Ack(delArgs);
            }
        }
    }
}