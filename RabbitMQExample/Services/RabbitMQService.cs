﻿using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace RabbitMQExample.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MyQueue",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty,
                              routingKey: "MyQueue",
                              mandatory: true,
                              basicProperties: null,
                              body: body);
            }
        }
    }
}
