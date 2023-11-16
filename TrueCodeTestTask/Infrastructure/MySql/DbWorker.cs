namespace TrueCodeTestTask.Infrastructure.MySql
{
    public static class DbWorker
    {
        public static void Initialize()
        {
            using (var context = new DbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        public static void InsertRequestLog(RequestLog requestLog)
        {
            using (var context = new DbContext())
            {
                context.RequestLogs!.Add(requestLog);
                context.SaveChanges();
            }
        }
    }
}
