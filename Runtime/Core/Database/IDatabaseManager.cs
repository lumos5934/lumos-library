using System.Collections.Generic;

namespace LumosLib
{
    public interface IDatabaseManager
    {
        public void RegisterData<T>() where T : BaseData;
        public List<T> GetAll<T>() where T : BaseData;
        public T Get<T>(int tableID) where T : BaseData;
    }
}