using TrueCodeTestTask.Infrastructure.Http;
using TrueCodeTestTask.Infrastructure.MySql;

namespace TrueCodeTestTask.Domain.Handlers
{
    public static class RequestHandler
    {
        public static async Task Send(string url)
        {
            try
            {
                var requestLog = new RequestLog
                {
                    Url = url,
                    Date = DateTimeOffset.UtcNow
                };

                requestLog.Response = await StaticHttpClient.SendGetRequest(url, true);

                DbWorker.InsertRequestLog(requestLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
