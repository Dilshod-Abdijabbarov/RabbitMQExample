using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQExample.Services
{
    public class RabbitMQConsumerService : BackgroundService
    {
        private IModel _channel;
        private readonly IConnection _connection;

        public RabbitMQConsumerService()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                Debug.WriteLine($"Navbatlar ketma-ketligi----------------------------------------: {content}");

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("MyQueue", false, consumer);

            Debug.WriteLine($"Navbatlar ketma-ketligi----------------------------------------: STOP ");

            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
