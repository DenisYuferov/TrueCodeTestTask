using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TrueCodeTestTask.Infrastructure.RabbitMq
{
    public class Broker : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channelModel;
        public Broker(string hostName, string queue)
        {
            if (string.IsNullOrWhiteSpace(hostName))
            {
                throw new ArgumentException($"'{nameof(hostName)}' cannot be null or whitespace.", nameof(hostName));
            }

            if (string.IsNullOrWhiteSpace(queue))
            {
                throw new ArgumentException($"'{nameof(queue)}' cannot be null or whitespace.", nameof(queue));
            }

            var factory = new ConnectionFactory { HostName = hostName, UserName = "rabbitmq", Password = "rabbitmq1234" };
            _connection = factory.CreateConnection();

            _channelModel = _connection.CreateModel();

            _channelModel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

        }

        public void Produce(string message, bool logProduce = false)
        {

            var bytes = Encoding.UTF8.GetBytes(message);

            _channelModel.BasicPublish(exchange: string.Empty, routingKey: _channelModel.CurrentQueue, basicProperties: null, body: bytes);

            if (logProduce)
            {
                Console.WriteLine($"Broker produce - Queue: {_channelModel.CurrentQueue} Message: {message}");
            }
        }

        public void RunConsume(Func<string, Task> action, bool logConsume = false)
        {
            var consumer = new EventingBasicConsumer(_channelModel);
            
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                if (logConsume)
                {
                    Console.WriteLine($"Broker consume - Queue: {_channelModel.CurrentQueue} Message: {message}");
                }

                await action(message);
            };

            Console.WriteLine($"Consumer is starting on: {_connection.Endpoint.HostName} with queue: {_channelModel.CurrentQueue}");

            _channelModel.BasicConsume(queue: _channelModel.CurrentQueue, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channelModel.Close();
            _channelModel.Dispose();

            _connection.Close();
            _connection.Dispose();
        }
    }
}
