namespace TrueCodeTestTask.Infrastructure.Http
{
    public static class StaticHttpClient
    {
        private static readonly HttpClient _httpClient;
        static StaticHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public static async Task<string> SendGetRequest(string url, bool logRequesting = false)
        {
            if (logRequesting)
            {
                Console.WriteLine($"Http sending get request to url: {url}");
            }

            var responseMessage = await _httpClient.GetAsync(url);
            var result = await responseMessage.Content.ReadAsStringAsync();

            if (logRequesting)
            {
                Console.WriteLine($"Http get response: {responseMessage.StatusCode}");
            }

            return result;
        }
    }
}
