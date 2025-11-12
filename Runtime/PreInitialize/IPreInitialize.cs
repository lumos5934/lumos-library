using System.Collections;

namespace LumosLib
{
    public interface IPreInitialize
    {
        public int PreInitOrder { get; }
        public IEnumerator InitAsync();
    }
}