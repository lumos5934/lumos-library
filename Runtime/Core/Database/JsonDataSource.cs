using System.IO;
using System.Threading.Tasks;

namespace LumosLib
{
    public class JsonDataSource : BaseDataSource
    {
        public JsonDataSource(string path) : base(path)
        {
        }

        private string GetPath(string key)
        {
            return Path.Combine(_path, $"{key}.json");
        }
        
        public override async Task PutAsync(string key, string json)
        {
            await File.WriteAllTextAsync(GetPath(key), json);
        }

        public override async Task<string> GetAsync(string key)
        {
            string path = GetPath(key);
            
            if (!File.Exists(path)) return null;

            return await File.ReadAllTextAsync(path);
        }
    }
}