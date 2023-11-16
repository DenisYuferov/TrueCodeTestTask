using TrueCodeTestTask.Domain.Model;
using TrueCodeTestTask.Infrastructure.FileManager;
using TrueCodeTestTask.Infrastructure.RabbitMq;

namespace TrueCodeTestTask.Domain.Handlers
{
    public static class ProduceHandler
    {
        public static async Task ProduceFile(string fileName)
        {
            try
            {
                var urls = FileHandler.GetLines(fileName);

                foreach (var url in urls)
                {
                    using var producer = new Broker(BrokerOptions.HostName, url.GetQueueByUrl());
                    producer.Produce(url, true);

                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
