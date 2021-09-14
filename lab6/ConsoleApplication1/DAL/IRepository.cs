using System.Collections.Generic;

namespace DAL
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();

        T Get(int ind);
        int Create(T entity);
        void Update(T entity);
        void Delete(int id);
        
        bool CheckIfExsits(int Id);
        
    }
}