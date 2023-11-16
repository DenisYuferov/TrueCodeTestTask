namespace TrueCodeTestTask.Infrastructure.FileManager
{
    public static class FileHandler
    {
        public static void PerformChecks(string[] fileNames)
        {
            if (fileNames?.FirstOrDefault() == null)
            {
                throw new Exception("File name has not been specified");
            }

            var fileName = fileNames[0];
            if (!File.Exists(fileName))
            {
                throw new Exception($"File has not been found: {fileName}");
            }

            var fileInfo = new FileInfo(fileName);
            if (fileInfo.Length == 0)
            {
                throw new Exception($"File is empty: {fileName}");
            }
        }

        public static List<string> GetLines(string fileName)
        {
            var result = File.ReadAllLines(fileName).ToList();

            return result;
        }
    }
}
