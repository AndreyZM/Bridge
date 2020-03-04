using System.IO;
namespace Bridge.Translator
{
    public class RewriterCache
    {
        public void Cache(string fileName, string content)
        {
            var cachedFileName = GetCachedFileName(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(cachedFileName));
            File.WriteAllText(cachedFileName, content);
        }

        public string TryGet(string fileName)
        {
            var fileTime = File.GetLastWriteTimeUtc(fileName);
            var cacheTime = File.GetLastWriteTimeUtc(GetCachedFileName(fileName));
            if (fileTime > cacheTime)
                return null;
            return File.ReadAllText(GetCachedFileName(fileName));
        }

        private string GetCachedFileName(string fileName)
        {
            return Path.Combine(Path.GetTempPath(), "Bridge.Net" , fileName.Replace(Path.GetPathRoot(fileName), ""));
        }
    }
}