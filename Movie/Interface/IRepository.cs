using System.Collections.Generic;

namespace Movie.Interface
{
    public interface IRepository<T>
    {
        void Add(T obj);
        
        void Update(int id, T newObj);
        
        void Delete(T obj);
        void Delete(int id);
        
        List<T> GetAll();
        T GetId(int id);

    }
}
