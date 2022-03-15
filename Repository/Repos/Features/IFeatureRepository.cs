using Domain.Entities;
using System.Collections.Generic;

namespace Repository
{
    public interface IFeatureRepository
    {
        bool Add(Feature obj);

        IEnumerable<Feature> GetAll(PaginationData p);
        int GetAllCount();
        Feature GetBy(int id);
        Feature GetBy(string name);
        IEnumerable<Feature> GetByDefaultAssociatedRole(int role);
        IEnumerable<int> GetIdsByDefaultAssociatedRole(int role);

        bool Update(Feature obj);

        bool RemoveBy(int id);
        bool RemoveBy(string name);
    }
}
