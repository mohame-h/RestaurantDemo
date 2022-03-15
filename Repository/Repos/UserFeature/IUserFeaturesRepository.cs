using Domain.Entities;
using System.Collections.Generic;

namespace Repository
{
    public interface IUserFeaturesRepository
    {
        bool Add(UserFeatures obj);
        bool AddRange(int userId, IEnumerable<int> featuresId);

        IEnumerable<UserFeatures> GetByUserId(int id);
        IEnumerable<UserFeatures> GetByFeatureId(int id);
        int GetAllCountByUserId(int Id);
        int GetAllCountByFeatureId(int Id);
        UserFeatures GetBy(UserFeatures obj);

        bool RemoveBy(UserFeatures obj);
        bool RemoveByUserId(int id);
        bool RemoveByFeatureId(int id);
    }
}
