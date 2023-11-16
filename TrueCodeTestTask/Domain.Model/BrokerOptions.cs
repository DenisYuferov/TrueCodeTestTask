namespace TrueCodeTestTask.Domain.Model
{
    public static class BrokerOptions
    {
        public static readonly string HostName = "localhost";
        public static readonly Dictionary<string, string> Queues = new Dictionary<string, string>
        {
            { ".ru",  "ruUrls"},
            { ".com",  "comUrls"},
            { "other", "otherUrls"}
        };

        public static string GetQueueByUrl(this string url)
        {
            foreach (var pair in Queues) 
            { 
                if (pair.Key == "other")
                {
                    continue;
                }

                if (url.Contains(pair.Key))
                {
                    return pair.Value;
                }
            }

            return Queues["other"];
        }
    }
}
