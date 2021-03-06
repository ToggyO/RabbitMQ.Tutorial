using System;
using RabbitMQ.Client;

namespace RabbitMQ.Consumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://rabbitmq:rabbitmq@localhost:5672")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            FanoutExchangeConsumer.Consume(channel);
        }
    }
}
