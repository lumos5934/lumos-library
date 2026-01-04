using System.Threading.Tasks;

namespace LumosLib
{
    public abstract class BaseDataSource
    {
        protected string _path;

        public BaseDataSource(string path)
        {
            _path = path;
        }
        
        public abstract Task PutAsync(string key, string json);
        public abstract Task<string> GetAsync(string key);
    }
}