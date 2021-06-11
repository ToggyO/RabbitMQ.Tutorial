using System;
using RabbitMQ.Client;

namespace RabbitMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672")
            };
            
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            FanoutExchangeProducer.Publish(channel);
        }
    }
}
