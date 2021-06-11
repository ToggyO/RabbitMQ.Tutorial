using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using RabbitMQ.Client;

namespace RabbitMQ.Producer
{
    public static class FanoutExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000}
            };
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Fanout, arguments: ttl);
            int count = 0;

            while (true)
            {
                var message = new {Name = "Producer", Message = $"Hello count: {count}"};
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> {{"acount", "new"}};
                
                channel.BasicPublish("demo-fanout-exchange", String.Empty, properties, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}