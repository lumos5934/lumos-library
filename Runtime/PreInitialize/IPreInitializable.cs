using Cysharp.Threading.Tasks;

namespace LLib
{
    public interface IPreInitializable
    {
        UniTask<bool> InitAsync(PreInitContext ctx);
    }
}