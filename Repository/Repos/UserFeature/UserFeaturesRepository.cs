using Dapper;
using Domain.Entities;
using Repository.QueriesConnectionFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Repository.Users
{
    public class UserFeaturesRepository : IUserFeaturesRepository
    {
        private DBEntities _dbCommands;
        private IQueryConnection _dbQueriesConnection;
        private IDbConnection _dbQueries;

        public UserFeaturesRepository(DBEntities dbCommands, IQueryConnection dbQueriesConnection)
        {
            _dbCommands = dbCommands;
            _dbQueriesConnection = dbQueriesConnection;
            _dbQueries = _dbQueriesConnection.OpenConnection();
        }

        public bool Add(UserFeatures obj)
        {
            try
            {
                _dbCommands.UserFeatures.Add(obj);
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public bool AddRange(IEnumerable<UserFeatures> obj)
        //{
        //    try
        //    {
        //        _dbCommands.UserFeatures.AddRange(obj);
        //        _dbCommands.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public bool AddRange(int userId, IEnumerable<int> featuresId)
        {
            var userFeaturesList = new List<UserFeatures>();
            featuresId.ToList().ForEach(z =>
            {
                var obj = new UserFeatures() { FeatureId = z, UserId = userId };
                userFeaturesList.Add(obj);
            });

            using (var transaction = _dbCommands.Database.BeginTransaction())
            {
                try
                {
                    _dbCommands.UserFeatures.AddRange(userFeaturesList);
                    _dbCommands.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public int GetAllCountByFeatureId(int Id)
        {
            var getUserFeaturesSQL = @"SELECT COUNT(FeatureId)
                                    FROM UserFeatures
                                    WHERE FeatureId = @Id";
            var result = _dbQueries.ExecuteScalar<int>(getUserFeaturesSQL, new { Id });
            return result;
        }

        public int GetAllCountByUserId(int Id)
        {
            var getUserFeaturesSQL = @"SELECT COUNT(UserId)
                                    FROM UserFeatures
                                    WHERE UserId = @Id";
            var result = _dbQueries.ExecuteScalar<int>(getUserFeaturesSQL, new { Id });
            return result; ;
        }

        public UserFeatures GetBy(UserFeatures obj)
        {
            var getUserFeatureSQL = @"SELECT UserId, FeatureId
                                    FROM UserFeatures
                                    WHERE UserId = @userId AND FeatureId= @featureId";
            var result = _dbQueries.QueryFirstOrDefault<UserFeatures>(getUserFeatureSQL, new { userId = obj.UserId, featureId = obj.FeatureId });
            return result;
        }

        public IEnumerable<UserFeatures> GetByFeatureId(int id)
        {
            var getUserFeatureSQL = @"SELECT UserId, FeatureId
                                    FROM UserFeatures
                                    WHERE FeatureId= @featureId";
            var result = _dbQueries.Query<UserFeatures>(getUserFeatureSQL, new { featureId = id });
            return result;
        }

        public IEnumerable<UserFeatures> GetByUserId(int id)
        {
            var getUserFeatureSQL = @"SELECT UserId, FeatureId
                                    FROM UserFeatures
                                    WHERE UserId = @userId";
            var result = _dbQueries.Query<UserFeatures>(getUserFeatureSQL, new { userId = id });
            return result;
        }

        public bool RemoveBy(UserFeatures obj)
        {
            var userFeature = _dbCommands.UserFeatures.FirstOrDefault(z => z.UserId == obj.UserId && z.FeatureId == obj.FeatureId);
            if (userFeature == null)
                return false;

            try
            {
                _dbCommands.UserFeatures.Remove(userFeature);
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveByFeatureId(int id)
        {
            var usersFeature = _dbCommands.UserFeatures.Where(z => z.FeatureId == id);
            if (usersFeature == null)
                return false;

            try
            {
                _dbCommands.UserFeatures.RemoveRange(usersFeature);
                _dbCommands.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveByUserId(int id)
        {
            var userFeatures = _dbCommands.UserFeatures.Where(z => z.UserId == id);
            if (userFeatures == null)
                return false;

            using (var transaction = _dbCommands.Database.BeginTransaction())
            {
                try
                {
                    _dbCommands.UserFeatures.RemoveRange(userFeatures);
                    _dbCommands.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
