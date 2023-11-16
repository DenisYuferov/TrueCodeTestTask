using TrueCodeTestTask.Domain.Handlers;
using TrueCodeTestTask.Domain.Model;
using TrueCodeTestTask.Infrastructure.FileManager;
using TrueCodeTestTask.Infrastructure.MySql;
using TrueCodeTestTask.Infrastructure.RabbitMq;

namespace TrueCodeTestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Application is starting...");

                DbWorker.Initialize();

                FileHandler.PerformChecks(args);

                var consumers = new List<Broker>();
                foreach (var queue in BrokerOptions.Queues)
                {
                    var consumer = new Broker(BrokerOptions.HostName, queue.Value);
                    consumers.Add(consumer);
                    consumer.RunConsume(RequestHandler.Send, true);
                }

                Task.Run(async () =>
                {
                    await ProduceHandler.ProduceFile(args[0]);
                });

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}