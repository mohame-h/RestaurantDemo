using System.Collections.Generic;

namespace Repository
{
    public interface IMainRepository<T, P>
    {
        bool Add(T obj);

        IEnumerable<T> GetAll(P p);
        int GetAllCount();
        T GetBy(int id);
        T GetBy(string name);

        bool Update(T obj);

        bool RemoveBy(int id);
        bool RemoveBy(string name);
    }
}
