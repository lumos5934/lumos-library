using Cysharp.Threading.Tasks;

namespace LLib
{
    public interface ISaveManager
    {
        public UniTask SaveAsync<T>(T data);
        public UniTask<T> LoadAsync<T>();
    }
}