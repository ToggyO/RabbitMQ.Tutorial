using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using RabbitMQ.Client;

namespace RabbitMQ.Producer
{
    public static class TopicExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000}
            };
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic, arguments: ttl);
            int count = 0;

            while (true)
            {
                var message = new {Name = "Producer", Message = $"Hello count: {count}"};
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            
                channel.BasicPublish("demo-topic-exchange", "account.init", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}